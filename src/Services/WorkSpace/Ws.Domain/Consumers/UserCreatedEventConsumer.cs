using Idn.Contracts.Events;
using MediatR;
using Ws.DataAccess;

namespace Ws.Domain.Consumers;

internal sealed class UserCreatedEventConsumer : INotificationHandler<UserCreatedEvent>
{
    private readonly IWorkspaceRepository _repository;

    public UserCreatedEventConsumer(IWorkspaceRepository repository) =>
        _repository = repository;
    
    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        var result = await _repository.GetWorkspaceAsync(notification.Id);

        if (result.Value is not Workspace)
        {
            const string defaultName = "workspace1";

           var result1 = await _repository.CreateWorkspaceAsync(notification.Id, defaultName);
        }
    }
}