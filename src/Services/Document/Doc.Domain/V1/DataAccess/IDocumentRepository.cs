using Badger.Sql.Abstractions.Errors;
using Col.Contracts.V1;
using Doc.Contracts.V1;
using OneOf;

namespace Doc.Domain.V1.DataAccess;

internal interface IDocumentRepository
{
    Task<OneOf<IReadOnlyList<Document>, UnreachableError>> ListDocumentsAsync(CollectionId collectionId);
}