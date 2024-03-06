using Badger.Clock;
using Col.Contracts.V1;
using Col.Domain.V1.DataAccess;
using Idn.Contracts.V1.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Col.Domain.V1.Consumers;

internal sealed class UserCreatedEventConsumer(
    ICollectionRepository repository,
    ILogger<UserCreatedEventConsumer> logger,
    IClock clock)
    : INotificationHandler<UserRegisteredEvent>
{
    public async Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
    {
        var listResult = await repository.ListAsync(notification.Id);

        if (listResult.IsT1)
        {
            logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(ICollectionRepository), listResult.AsT1.Value);
            
            return;
        }
        
        if (listResult.AsT0.Any())
        {
            return;
        }
        
        const string defaultName = "My collection";
        const string defaultIcon = "#";

        var registerResult = await repository.RegisterAsync(
            notification.Id,
            CollectionId.New(),
            defaultName,
            defaultIcon,
            EncryptionSide.Client,
            clock.Now,
            version: 1);

        if (registerResult.IsT1)
        {
            logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(ICollectionRepository), listResult.AsT1.Value);
        }
    }
}