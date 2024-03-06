using Badger.OneOf.Types;
using Col.Contracts.V1;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Doc.Contracts.V1;

public sealed record RegisterDocumentRequest(CollectionId CollectionId, Document Document, File[] files) : IRequest<OneOf<Success, NotFound<CollectionId>, Unreachable>>;