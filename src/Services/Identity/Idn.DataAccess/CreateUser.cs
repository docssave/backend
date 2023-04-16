using Idn.Contracts;

namespace Idn.DataAccess;

public sealed class CreateUser
{
    public CreateUser(string name, string encryptedEmail, AuthorizationSource source, string sourceUserId)
    {
        Name = name;
        EncryptedEmail = encryptedEmail;
        Source = source;
        SourceUserId = sourceUserId;
    }
    
    public string Name { get; }
    
    public string EncryptedEmail { get; }
    
    public AuthorizationSource Source { get; }

    public string SourceUserId { get; set; }
}