using MediatR;

namespace Idn.Contracts.Events;

public sealed class UserRegisteredEvent : INotification
{
    public UserRegisteredEvent(UserId id, DateTimeOffset registeredAt)
    {
        Id = id;
        this.RegisteredAt = registeredAt;
    }
    
    public UserId Id { get; }

    public DateTimeOffset RegisteredAt { get; }
}