using Badger.OneOf.Types;
using Col.Contracts.V1;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Doc.Contracts.V1;

public sealed record RegisterDocumentRequest(
    CollectionId CollectionId,
    DocumentId DocumentId,
    string Name,
    string Icon,
    long? ExpectedVersion,
    File[] files)
    : IRequest<OneOf<Success, Unknown, NotFound, Unreachable>>;