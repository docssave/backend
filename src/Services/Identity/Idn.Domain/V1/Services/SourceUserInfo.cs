namespace Idn.Domain.V1.Services;

internal sealed class SourceUserInfo
{
    public SourceUserInfo(string id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    public string Id { get; }
    
    public string Name { get; }
    
    public string Email { get; }
}