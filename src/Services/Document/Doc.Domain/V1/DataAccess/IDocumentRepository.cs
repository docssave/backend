using Badger.Sql.Error;
using Col.Contracts.V1;
using Doc.Contracts.V1;
using OneOf;
using OneOf.Types;

namespace Doc.Domain.V1.DataAccess;

internal interface IDocumentRepository
{
    Task<OneOf<IReadOnlyList<Document>, UnreachableDatabaseError>> ListDocumentsAsync(CollectionId collectionId);

    Task<OneOf<Success, UnreachableDatabaseError>> RegisterDocumentAsync(Document document);
}