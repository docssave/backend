using MediatR;

namespace Idn.Contracts.Events;

public sealed class UserCreatedEvent : INotification
{
    public UserCreatedEvent(long id)
    {
        Id = id;
    }
    
    public long Id { get; }
}