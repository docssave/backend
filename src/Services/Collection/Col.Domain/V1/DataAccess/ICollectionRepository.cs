using Badger.OneOf.Types;
using Col.Contracts.V1;
using Idn.Contracts.V1;
using OneOf;
using OneOf.Types;

namespace Col.Domain.V1.DataAccess;

public interface ICollectionRepository
{
    Task<OneOf<IReadOnlyList<Collection>, Unreachable<string>>> ListAsync(UserId userId);

    Task<OneOf<Success, Conflict, Unreachable<string>>> RegisterAsync(
        UserId userId,
        CollectionId id,
        string name,
        string icon,
        EncryptionSide encryptionSide,
        DateTimeOffset addedAt,
        int? expectedVersion,
        int nextVersion);

    Task<OneOf<Success, NotFound, Unreachable<string>>> CheckExistingAsync(CollectionId collectionId);
}