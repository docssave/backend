using Badger.OneOf.Types;
using MediatR;
using OneOf;

namespace Col.Contracts.V1;

public sealed record RegisterCollectionRequest(CollectionId Id, string Name, string Icon, EncryptionSide EncryptionSide, int? Version)
    : IRequest<OneOf<Collection, Unknown, Conflct, Unreachable>>;