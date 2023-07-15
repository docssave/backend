using Badger.Sql.Abstractions.Errors;
using Col.Contracts;
using Idn.Contracts;
using OneOf;
using OneOf.Types;

namespace Col.Domain.DataAccess;

public interface ICollectionRepository
{
    Task<OneOf<IReadOnlyList<Collection>, UnreachableError>> ListAsync(UserId userId);

    Task<OneOf<Success, UnreachableError>> RegisterAsync(
        UserId userId,
        CollectionId id,
        string name,
        string icon,
        EncryptionSide encryptionSide,
        DateTimeOffset addedAt,
        int version);
}