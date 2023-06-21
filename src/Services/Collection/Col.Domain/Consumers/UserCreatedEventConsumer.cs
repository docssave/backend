using Col.Contracts;
using Col.DataAccess;
using Idn.Contracts.Events;
using MediatR;

namespace Col.Domain.Consumers;

internal sealed class UserCreatedEventConsumer : INotificationHandler<UserCreatedEvent>
{
    private readonly ICollectionRepository _repository;

    public UserCreatedEventConsumer(ICollectionRepository repository) => 
        _repository = repository;
    
    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        var collections = await _repository.ListCollectionsAsync(notification.Id);

        if (!collections.Any())
        {
            const string defaultName = "My collection";
            const string defaultIcon = "#";

            await _repository.RegisterCollectionAsync(CollectionId.New(), defaultName, defaultIcon, EncryptSide.Client, version: 1);
        }
    }
}