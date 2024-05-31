using Badger.OneOf.Types;
using Doc.Contracts.V1;
using OneOf;
using OneOf.Types;
using File = Doc.Contracts.V1.File;

namespace Doc.Domain.V1.DataAccess;

internal interface IFileRepository
{
    Task<OneOf<Success, Unreachable<string>>> RegisterAsync(DocumentId documentId, File[] files, DateTimeOffset registeredAt);

    Task<OneOf<IReadOnlyList<File>, Unreachable<string>>> ListAsync(DocumentId documentId);

    Task<OneOf<Success, Unreachable<string>>> DeleteAsync(FileId[] fileIds);

    Task<OneOf<Success, Unreachable<string>>> DeleteDocumentFilesAsync(DocumentId documentId);
}