using Badger.Clock;
using Idn.Contracts.V1.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Ws.Contracts.V1;
using Ws.Domain.V1.DataAccess;

namespace Ws.Domain.V1.Consumers;

internal sealed class UserRegisteredEventConsumer : INotificationHandler<UserRegisteredEvent>
{
    private readonly IWorkspaceRepository _repository;
    private readonly ILogger<UserRegisteredEventConsumer> _logger;
    private readonly IClock _clock;

    public UserRegisteredEventConsumer(IWorkspaceRepository repository, ILogger<UserRegisteredEventConsumer> logger, IClock clock)
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
            _logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(IWorkspaceRepository), listResult.AsT1.Reason);
            
            return;
        }
        
        if (listResult.AsT0.Any())
        {
            return;
        }
        
        const string defaultName = "workspace1";

        var registerResult = await _repository.RegisterAsync(WorkspaceId.New(), defaultName, notification.Id, _clock.Now);
        
        if (registerResult.IsT1)
        {
            _logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(IWorkspaceRepository), listResult.AsT1.Reason);
        }
    }
}