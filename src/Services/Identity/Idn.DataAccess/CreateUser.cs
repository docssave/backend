using Idn.Contracts;

namespace Idn.DataAccess;

public sealed class CreateUser
{
    public CreateUser(string name, string encryptedEmail, AuthorizationSource source)
    {
        Name = name;
        EncryptedEmail = encryptedEmail;
        Source = source;
    }
    
    public string Name { get; }
    
    public string EncryptedEmail { get; }
    
    public AuthorizationSource Source { get; }
}