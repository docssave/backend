using MediatR;

namespace Idn.Contracts.Events;

public sealed class UserCreatedEvent : INotification
{
    public UserCreatedEvent(UserId id, DateTimeOffset registeredAt)
    {
        Id = id;
        this.RegisteredAt = registeredAt;
    }
    
    public UserId Id { get; }

    public DateTimeOffset RegisteredAt { get; }
}