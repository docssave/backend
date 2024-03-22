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

    public Task<OneOf<Success, NotFound, Unreachable<string>>> RegisterDocumentAsync(CollectionId collectionId, Document document, File[] files) =>
        connectionFactory.TryAsync(async (connection, transaction) =>
        {
            throw new NotImplementedException();
        }, ToUnreachableError);

    private static Unreachable<string> ToUnreachableError(Exception exception) => new(exception.Message);
    
    private sealed record DocumentEntity(Guid Id, string Name, string Icon, long Version, long RegisteredAtTimespan);
}