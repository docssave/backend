using Idn.Contracts;

namespace Idn.DataAccess;

public sealed class User
{
    public User(UserId id, string name, string encryptedEmail, AuthorizationSource source)
    {
        Id = id;
        Name = name;
        EncryptedEmail = encryptedEmail;
        Source = source;
    }
    
    public UserId Id { get; }
    
    public string Name { get; }
    
    public string EncryptedEmail { get; }
    
    public AuthorizationSource Source { get; }
}