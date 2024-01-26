using Doc.Contracts.V1.Events;
using Fl.Domain.V1.DataAccess;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fl.Domain.V1.Consumers;

internal sealed class DocumentDeletedEventConsumer(IFileRepository repository, ILogger<DocumentDeletedEventConsumer> logger) : INotificationHandler<DocumentDeletedEvent>
{
    public async Task Handle(DocumentDeletedEvent notification, CancellationToken cancellationToken)
    {
        var result = await repository.DeleteDocumentFilesAsync(notification.DocumentId);

        if (result.IsT1)
        {
            logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(IFileRepository), result.AsT1.Reason);
        }
    }
}