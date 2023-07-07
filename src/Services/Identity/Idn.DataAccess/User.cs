using Idn.Contracts;

namespace Idn.DataAccess;

public sealed class User
{
    public User(UserId id, string name, string encryptedEmail, AuthorizationSource source, DateTimeOffset registeredAt)
    {
        Id = id;
        Name = name;
        EncryptedEmail = encryptedEmail;
        Source = source;
        RegisteredAt = registeredAt;
    }
    
    public UserId Id { get; }
    
    public string Name { get; }
    
    public string EncryptedEmail { get; }
    
    public AuthorizationSource Source { get; }
    
    public DateTimeOffset RegisteredAt { get; }
}