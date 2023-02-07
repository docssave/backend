namespace Idn.DataAccess;

public interface IIdentityRepository
{
    Task<Result<User?>> GetUserAsync(string encryptedEmail);

    Task<Result<User>> CreateUserAsync(CreateUser user);
}