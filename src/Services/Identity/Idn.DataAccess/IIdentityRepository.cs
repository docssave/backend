using OneOf;
using OneOf.Types;
using Sql.Abstractions.Errors;

namespace Idn.DataAccess;

public interface IIdentityRepository
{
    Task<OneOf<User, NotFound, UnreachableError>> GetUserAsync(string sourceUserId);

    Task<OneOf<User, UnreachableError>> RegisterUserAsync(CreateUser createUser, DateTimeOffset registeredAt);
}