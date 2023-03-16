namespace Idn.DataAccess;

public sealed class IdentityRepository : IIdentityRepository
{
    public Task<Result<User?>> GetUserAsync(string encryptedEmail)
    {
        throw new NotImplementedException();
    }

    public Task<Result<User>> CreateUserAsync(CreateUser user)
    {
        throw new NotImplementedException();
    }
}