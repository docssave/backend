using Col.Contracts;
using Idn.Contracts;
using OneOf;
using OneOf.Types;
using Sql.Abstractions.Errors;

namespace Col.Domain.DataAccess;

internal interface ICollectionRepository
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