using Badger.Sql.Abstractions.Errors;
using Doc.Contracts.V1;
using OneOf;
using OneOf.Types;
using File = Fl.Contracts.V1.File;

namespace Fl.Domain.V1.DataAccess;

internal interface IFileRepository
{
    Task<OneOf<Success, UnreachableError>> RegisterAsync(DocumentId documentId, File[] files, DateTimeOffset registeredAt);

    Task<OneOf<IReadOnlyList<File>, UnreachableError>> ListAsync(DocumentId documentId);
}