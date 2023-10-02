using MediatR;
using OneOf;
using OneOf.Types;

namespace Col.Contracts.V1;

public sealed record RegisterCollectionRequest(CollectionId Id, string Name, string Icon, EncryptionSide EncryptionSide, int? Version) : IRequest<OneOf<Collection, Error<string>>>;