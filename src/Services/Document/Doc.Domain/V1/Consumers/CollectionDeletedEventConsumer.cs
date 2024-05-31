using Col.Contracts.V1.Events;
using Doc.Domain.V1.DataAccess;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Doc.Domain.V1.Consumers;

internal sealed class CollectionDeletedEventConsumer(
    IDocumentRepository documentRepository,
    ILogger<CollectionDeletedEventConsumer> logger)
    : INotificationHandler<CollectionDeletedEvent>
{
    public Task Handle(CollectionDeletedEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}