namespace Idn.Domain.V1.Services;

internal sealed class SourceUserInfo(string id, string name, string email)
{
    public string Id { get; } = id;

    public string Name { get; } = name;

    public string Email { get; } = email;
}