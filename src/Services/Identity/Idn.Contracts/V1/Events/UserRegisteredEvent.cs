using MediatR;

namespace Idn.Contracts.V1.Events;

public sealed class UserRegisteredEvent(UserId id, DateTimeOffset registeredAt) : INotification
{
    public UserId Id { get; } = id;

    public DateTimeOffset RegisteredAt { get; } = registeredAt;
}