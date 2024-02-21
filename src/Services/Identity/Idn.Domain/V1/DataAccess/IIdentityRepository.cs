using Badger.Sql.Error;
using Idn.Contracts.V1;
using OneOf;
using OneOf.Types;

namespace Idn.Domain.V1.DataAccess;

internal interface IIdentityRepository
{
    Task<OneOf<User, NotFound, UnreachableError>> GetUserAsync(string sourceUserId);

    Task<OneOf<User, UnreachableError>> RegisterUserAsync(string sourceUserId, string name, string encryptedEmail, AuthorizationSource source, DateTimeOffset registeredAt);
}