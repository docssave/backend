using Badger.OneOf.Types;
using MediatR;
using OneOf;
using Unknown = OneOf.Types.Unknown;

namespace Col.Contracts.V1;

public sealed record RegisterCollectionRequest(CollectionId Id, string Name, string Icon, EncryptionSide EncryptionSide, int? Version)
    : IRequest<OneOf<Collection, Unknown, Conflict, Unreachable>>;