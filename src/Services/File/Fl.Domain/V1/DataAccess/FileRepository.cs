using Badger.Collections.Extensions;
using Badger.Sql.Abstractions;
using Badger.Sql.Abstractions.Errors;
using Badger.Sql.Abstractions.Extensions;
using Dapper;
using Doc.Contracts.V1;
using Fl.Contracts.V1;
using OneOf;
using OneOf.Types;
using File = Fl.Contracts.V1.File;

namespace Fl.Domain.V1.DataAccess;

internal sealed class FileRepository(IDbConnectionFactory connectionFactory, SqlQueries queries) : IFileRepository
{
    public Task<OneOf<Success, UnreachableError>> RegisterAsync(DocumentId documentId, File[] files, DateTimeOffset registeredAt) =>
        connectionFactory.TryAsync(async (connection, transaction) =>
        {
            foreach (var file in files)
            {
                var registerFileQuery = queries.RegisterFileQuery(file.Id, file.Content);

                await connection.ExecuteAsync(registerFileQuery, transaction: transaction);

                var registerFileMetadataQuery = queries.RegisterFileMetadataQuery(file.Id, documentId, file.Content.LongLength, file.MimeType, registeredAt);

                await connection.ExecuteAsync(registerFileMetadataQuery, transaction: transaction);
            }

            return new Success();
        }, ToUnreachableError);

    public Task<OneOf<IReadOnlyList<File>, UnreachableError>> ListAsync(DocumentId documentId) => connectionFactory.TryAsync(async connection =>
    {
        var sqlQuery = queries.GetFilesQuery(documentId);

        var files = await connection.QueryAsync<FileEntity>(sqlQuery);

        return files
            .Select(file => new File(new FileId(file.Id), file.Content, file.Type))
            .ToReadOnlyList();
    }, ToUnreachableError);
    
    private static UnreachableError ToUnreachableError(Exception exception) => new(exception.Message);

    private sealed record FileEntity(Guid Id, byte[] Content, long Size, string Type, long RegisteredAtTimestamp);
}