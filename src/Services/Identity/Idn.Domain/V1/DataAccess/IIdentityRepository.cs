using Badger.OneOf.Types;
using Idn.Contracts.V1;
using OneOf;
using OneOf.Types;

namespace Idn.Domain.V1.DataAccess;

internal interface IIdentityRepository
{
    Task<OneOf<User, NotFound, Unreachable<string>>> GetUserAsync(string sourceUserId);

    Task<OneOf<User, Unreachable<string>>> RegisterUserAsync(string sourceUserId, string name, string encryptedEmail, AuthorizationSource source, DateTimeOffset registeredAt);
}