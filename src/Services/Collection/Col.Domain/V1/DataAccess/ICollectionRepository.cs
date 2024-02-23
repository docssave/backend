using Badger.Sql.Error;
using Col.Contracts.V1;
using Idn.Contracts.V1;
using OneOf;
using OneOf.Types;

namespace Col.Domain.V1.DataAccess;

public interface ICollectionRepository
{
    Task<OneOf<IReadOnlyList<Collection>, UnreachableDatabaseError>> ListAsync(UserId userId);

    Task<OneOf<Success, UnreachableDatabaseError>> RegisterAsync(
        UserId userId,
        CollectionId id,
        string name,
        string icon,
        EncryptionSide encryptionSide,
        DateTimeOffset addedAt,
        int version);
}