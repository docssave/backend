using Clock;
using Col.Contracts;
using Col.DataAccess;
using Col.Domain.DataAccess;
using Idn.Contracts.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Col.Domain.Consumers;

internal sealed class UserCreatedEventConsumer : INotificationHandler<UserRegisteredEvent>
{
    private readonly ICollectionRepository _repository;
    private readonly ILogger<UserCreatedEventConsumer> _logger;
    private readonly IClock _clock;

    public UserCreatedEventConsumer(
        ICollectionRepository repository,
        ILogger<UserCreatedEventConsumer> logger,
        IClock clock)
    {
        _repository = repository;
        _logger = logger;
        _clock = clock;
    }

    public async Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
    {
        var listResult = await _repository.ListAsync(notification.Id);

        if (listResult.IsT1)
        {
            _logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(ICollectionRepository), listResult.AsT1.Reason);
            
            return;
        }
        
        if (listResult.AsT0.Any())
        {
            return;
        }
        
        const string defaultName = "My collection";
        const string defaultIcon = "#";

        var registerResult = await _repository.RegisterAsync(
            notification.Id,
            CollectionId.New(),
            defaultName,
            defaultIcon,
            EncryptionSide.Client,
            _clock.Now,
            version: 1);

        if (registerResult.IsT1)
        {
            _logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(ICollectionRepository), listResult.AsT1.Reason);
        }
    }
}