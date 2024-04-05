using Badger.OneOf.Types;
using Doc.Contracts.V1;
using OneOf;
using OneOf.Types;
using File = Doc.Contracts.V1.File;

namespace Doc.Domain.V1.DataAccess;

internal interface IDocumentRepository
{
    Task<OneOf<IReadOnlyList<Document>, Unreachable<string>>> ListDocumentsAsync(Guid collectionId);

    Task<OneOf<Success, Unreachable<string>>> RegisterDocumentAsync(Guid collectionId, Document document, File[] files);
}