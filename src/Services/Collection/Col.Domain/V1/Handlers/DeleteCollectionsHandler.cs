using Badger.OneOf.Types;
using Col.Contracts.V1;
using Col.Contracts.V1.Events;
using Col.Domain.V1.DataAccess;
using Idn.Contracts.V1;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace Col.Domain.V1.Handlers;

internal sealed class DeleteCollectionsHandler(
    ICollectionRepository repository,
    IUserIdAccessor userIdAccessor,
    IMediator mediator,
    ILogger<DeleteCollectionsHandler> logger) 
    : IRequestHandler<DeleteCollectionsRequest, OneOf<Success, Unknown, Unreachable>>
{
    public async Task<OneOf<Success, Unknown, Unreachable>> Handle(DeleteCollectionsRequest request, CancellationToken cancellationToken)
    {
        var userId = userIdAccessor.UserId;

        if (userId == null)
        {
            logger.LogError("Could not get User from IUserIdAccessor");
            return new Unknown();
        }

        var result = await repository.DeleteCollectionsAsync(userId.Value, request.CollectionIds);

        if (result.IsT1)
        {
            logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(ICollectionRepository), result.AsT1.Value);
            return new Unreachable();
        }

        await mediator.Publish(new CollectionsDeletedEvent(result.AsT0.Value), cancellationToken);

        return new Success();
    }
}