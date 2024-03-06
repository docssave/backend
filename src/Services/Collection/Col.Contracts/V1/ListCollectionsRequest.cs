using Badger.OneOf.Types;
using MediatR;
using OneOf;

namespace Col.Contracts.V1;

public sealed record ListCollectionsRequest : IRequest<OneOf<IReadOnlyList<Collection>, Unknown, Unreachable>>;