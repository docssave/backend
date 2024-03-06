using Badger.OneOf.Types;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Col.Contracts.V1;

public sealed record CheckCollectionExistingRequest(CollectionId CollectionId) : IRequest<OneOf<Success, NotFound, Unreachable>>;