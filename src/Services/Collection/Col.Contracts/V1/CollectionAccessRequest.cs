using Badger.OneOf.Types;
using Idn.Contracts.V1;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Col.Contracts.V1;

public sealed record CollectionAccessRequest(UserId UserId, CollectionId CollectionId): IRequest<OneOf<Success, Forbidden, Unreachable>>;