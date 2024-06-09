using Col.Contracts.V1.Events;
using Doc.Domain.V1.DataAccess;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Doc.Domain.V1.Consumers;

internal sealed class CollectionsDeletedEventConsumer(
    IDocumentRepository documentRepository,
    ILogger<CollectionsDeletedEventConsumer> logger)
    : INotificationHandler<CollectionsDeletedEvent>
{
    public async Task Handle(CollectionsDeletedEvent notification, CancellationToken cancellationToken)
    {
        foreach (var collectionId in notification.CollectionIds)
        {
            var result = await documentRepository.DeleteDocumentsAsync(collectionId);

            if (result.IsT1)
            {
                logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(IDocumentRepository), result.AsT1.Value);
            }
        }
    }
}