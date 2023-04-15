using SqlServer;

namespace Idn.DataAccess;

public interface IIdentityRepository
{
    Task<RepositoryResult<User?>> GetUserAsync(string sourceUserId);

    Task<RepositoryResult<User>> CreateUserAsync(CreateUser user);
}