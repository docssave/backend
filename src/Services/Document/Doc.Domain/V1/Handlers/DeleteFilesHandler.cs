using Badger.OneOf.Types;
using Col.Contracts.V1;
using Doc.Contracts.V1;
using Doc.Domain.V1.DataAccess;
using Idn.Contracts.V1;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace Doc.Domain.V1.Handlers;

internal sealed class DeleteFilesHandler(
    IFileRepository repository,
    IMediator mediator,
    IUserIdAccessor userIdAccessor,
    ILogger<DeleteFilesHandler> logger) :
    IRequestHandler<DeleteFilesRequest, OneOf<Success, Unknown, Forbidden, Unreachable>>
{
    public async Task<OneOf<Success, Unknown, Forbidden, Unreachable>> Handle(DeleteFilesRequest request, CancellationToken cancellationToken)
    {
        var userId = userIdAccessor.UserId;

        if (userId == null)
        {
            logger.LogError("Could not get UserId from IUserIdAccessor");
            return new Unknown();
        }

        var checkCollection = await mediator.Send(new CheckCollectionExistingRequest(request.CollectionId), cancellationToken);

        if (checkCollection.IsT1)
        {
            return new Success();
        }

        var checkAccess = await mediator.Send(new CollectionAccessRequest(userId.Value, request.CollectionId), cancellationToken);

        if (checkAccess.IsT1)
        {
            return new Forbidden();
        }

        var result = await repository.DeleteAsync(request.FileIds);

        return result.Match(success => success, ToError);

        OneOf<Success, Unknown, Forbidden, Unreachable> ToError(Unreachable<string> error)
        {
            logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(IFileRepository), error.Value);
            return new();
        }
    }
}