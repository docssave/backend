using Badger.Sql.Error;
using Doc.Contracts.V1;
using OneOf;
using OneOf.Types;
using File = Doc.Contracts.V1.File;

namespace Doc.Domain.V1.DataAccess;

internal interface IFileRepository
{
    Task<OneOf<Success, UnreachableDatabaseError>> RegisterAsync(DocumentId documentId, File[] files, DateTimeOffset registeredAt);

    Task<OneOf<IReadOnlyList<File>, UnreachableDatabaseError>> ListAsync(DocumentId documentId);

    Task<OneOf<Success, UnreachableDatabaseError>> DeleteAsync(FileId fileId);

    Task<OneOf<Success, UnreachableDatabaseError>> DeleteDocumentFilesAsync(DocumentId documentId);
}