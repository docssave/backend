using MediatR;

namespace Idn.Contracts.Events;

public sealed class UserCreatedEvent : INotification
{
    public UserCreatedEvent(UserId id)
    {
        Id = id;
    }
    
    public UserId Id { get; }
}