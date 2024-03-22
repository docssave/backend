using Badger.Clock;
using Idn.Contracts.V1.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Ws.Contracts.V1;
using Ws.Domain.V1.DataAccess;

namespace Ws.Domain.V1.Consumers;

internal sealed class UserRegisteredEventConsumer(IWorkspaceRepository repository, ILogger<UserRegisteredEventConsumer> logger, IClock clock)
    : INotificationHandler<UserRegisteredEvent>
{
    public async Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
    {
        var listResult = await repository.ListAsync(notification.Id);

        if (listResult.IsT1)
        {
            logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(IWorkspaceRepository), listResult.AsT1.Value);
            
            return;
        }
        
        if (listResult.AsT0.Any())
        {
            return;
        }
        
        const string defaultName = "workspace1";

        var registerResult = await repository.RegisterAsync(WorkspaceId.New(), defaultName, notification.Id, clock.Now);
        
        if (registerResult.IsT1)
        {
            logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(IWorkspaceRepository), listResult.AsT1.Value);
        }
    }
}