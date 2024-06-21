using Badger.Collections.Extensions;
using Badger.OneOf.Types;
using Badger.Sql.Abstractions;
using Badger.Sql.Abstractions.Extensions;
using Col.Contracts.V1;
using Dapper;
using Doc.Contracts.V1;
using OneOf;
using OneOf.Types;
using File = Doc.Contracts.V1.File;

namespace Doc.Domain.V1.DataAccess;

internal sealed class DocumentRepository(IDbConnectionFactory connectionFactory, SqlQueries queries) : IDocumentRepository
{
    public Task<OneOf<IReadOnlyList<Document>, Unreachable<string>>> ListDocumentsAsync(CollectionId collectionId) =>
        connectionFactory.TryAsync(async connection =>
        {
            var entities = await connection.QueryAsync<DocumentEntity>(queries.GetDocumentsQuery(collectionId));

            return entities
                .Select(entity => new Document(
                    new DocumentId(entity.Id),
                    entity.Name,
                    entity.Icon,
                    entity.Version,
                    DateTimeOffset.FromUnixTimeMilliseconds(entity.RegisteredAtTimespan)))
                .ToReadOnlyList();
        }, ToUnreachableError);

    public Task<OneOf<Success, Conflict, Unreachable<string>>> RegisterDocumentAsync(CollectionId collectionId, Document document, File[] files) =>
        connectionFactory.TryAsync(async (connection, transaction) =>
        {
            var existingVersion = await connection.QuerySingleAsync<long?>(queries.GetDocumentVersionQuery(document.DocumentId), transaction: transaction);

            if (existingVersion.HasValue && existingVersion != document.Version)
            {
                return OneOf<Success, Conflict>.FromT1(new Conflict());
            }

            await connection.ExecuteAsync(queries.GetRegisterDocumentQuery(document, collectionId));

            foreach (var file in files)
            {
                var registerFileQuery = queries.RegisterFileQuery(file.Id, file.Content);

                await connection.ExecuteAsync(registerFileQuery, transaction: transaction);

                var registerFileMetadataQuery = queries.RegisterFileMetadataQuery(file.Id, document.DocumentId, file.Content.LongLength, file.MimeType, document.RegisteredAt);

                await connection.ExecuteAsync(registerFileMetadataQuery, transaction: transaction);
            }

            return OneOf<Success, Conflict>.FromT0(new Success());
        }, ToUnreachableError);

    public Task<OneOf<Success, Unreachable<string>>> DeleteDocumentsAsync(CollectionId collectionId, DocumentId[]? documentIds = null) => connectionFactory.TryAsync(async connection =>
    {
        await connection.ExecuteAsync(queries.GetDeleteDocumentsQuery(collectionId));

        return new Success();
    }, ToUnreachableError);

    private static Unreachable<string> ToUnreachableError(Exception exception) => new(exception.Message);

    private sealed record DocumentEntity(Guid Id, string Name, string Icon, long Version, long RegisteredAtTimespan);
}