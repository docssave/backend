using Idn.Contracts;
using OneOf;
using OneOf.Types;
using Sql.Abstractions.Errors;

namespace Idn.Domain.DataAccess;

internal interface IIdentityRepository
{
    Task<OneOf<User, NotFound, UnreachableError>> GetUserAsync(string sourceUserId);

    Task<OneOf<User, UnreachableError>> RegisterUserAsync(string sourceUserId, string name, string encryptedEmail, AuthorizationSource source, DateTimeOffset registeredAt);
}