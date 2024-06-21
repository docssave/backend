using Badger.OneOf.Types;
using Col.Contracts.V1;
using Doc.Contracts.V1;
using Doc.Domain.V1.DataAccess;
using Idn.Contracts.V1;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;
using File = Doc.Contracts.V1.File;

namespace Doc.Domain.V1.Handlers;

internal sealed class ListFilesHandler(
    IFileRepository repository,
    ISender mediator,
    IUserIdAccessor userIdAccessor,
    ILogger<ListFilesHandler> logger)
    : IRequestHandler<ListFilesRequest, OneOf<IReadOnlyList<File>, Unknown, Forbidden, Unreachable>>
{
    public async Task<OneOf<IReadOnlyList<File>, Unknown, Forbidden, Unreachable>> Handle(ListFilesRequest request, CancellationToken cancellationToken)
    {
        var userId = userIdAccessor.UserId;

        if (userId == null)
        {
            logger.LogError("Could not get UserId from IUserIdAccessor");
            return new Unknown();
        }
        
        var checkAccess = await mediator.Send(new CollectionAccessRequest(userId.Value, request.CollectionId), cancellationToken);

        if (checkAccess.IsT1)
        {
            return new Forbidden();
        }

        var result = await repository.ListAsync(request.DocumentId);

        return result.Match(OneOf<IReadOnlyList<File>, Unknown, Forbidden, Unreachable>.FromT0, ToError);

        OneOf<IReadOnlyList<File>, Unknown, Forbidden, Unreachable> ToError(Unreachable<string> error)
        {
            logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(IFileRepository), error.Value);
            return new();
        }
    }
}