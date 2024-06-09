using Badger.OneOf.Types;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Col.Contracts.V1;

public sealed record DeleteCollectionsRequest(CollectionId[] CollectionIds) : IRequest<OneOf<Success, Unknown, Unreachable>>;