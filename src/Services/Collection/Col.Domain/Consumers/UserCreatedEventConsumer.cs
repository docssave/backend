using Col.DataAccess;
using Idn.Contracts.Events;
using MediatR;

namespace Col.Domain.Consumers;

internal sealed class UserCreatedEventConsumer : INotificationHandler<UserCreatedEvent>
{
    private readonly ICollectionRepository _repository;

    public UserCreatedEventConsumer(ICollectionRepository repository) => 
        _repository = repository;
    
    public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        var result = await _repository.ListCollectionsAsync(notification.Id);
    }
}