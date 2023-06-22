using Idn.Contracts;
using Col.Contracts;
using OneOf;
using OneOf.Types;
using Sql.Abstractions.Errors;

namespace Col.DataAccess;

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