using Badger.Sql.Error;
using Col.Contracts.V1;
using Doc.Contracts.V1;
using OneOf;
using OneOf.Types;
using File = Doc.Contracts.V1.File;

namespace Doc.Domain.V1.DataAccess;

internal interface IDocumentRepository
{
    Task<OneOf<IReadOnlyList<Document>, UnreachableDatabaseError>> ListDocumentsAsync(CollectionId collectionId);

    Task<OneOf<Success, NotFoundDatabaseError, UnreachableDatabaseError>> RegisterDocumentAsync(CollectionId collectionId, Document document, File[] files);
}