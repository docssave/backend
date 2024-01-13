using Idn.Contracts.V1;

namespace Idn.Domain.V1.DataAccess;

public sealed class User(UserId id, string name, string encryptedEmail, AuthorizationSource source, DateTimeOffset registeredAt)
{
    public UserId Id { get; } = id;

    public string Name { get; } = name;

    public string EncryptedEmail { get; } = encryptedEmail;

    public AuthorizationSource Source { get; } = source;

    public DateTimeOffset RegisteredAt { get; } = registeredAt;
}