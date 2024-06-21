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

internal sealed class DeleteDocumentsHandler(
    IDocumentRepository repository,
    ISender sender,
    IUserIdAccessor userIdAccessor,
    ILogger<DeleteDocumentsHandler> logger)
    : IRequestHandler<DeleteDocumentsRequest, OneOf<Success, Unknown, Forbidden, Unreachable>>
{
    public async Task<OneOf<Success, Unknown, Forbidden, Unreachable>> Handle(DeleteDocumentsRequest request, CancellationToken cancellationToken)
    {
        var userId = userIdAccessor.UserId;

        if (userId == null)
        {
            logger.LogError("Could not get UserId from IUserIdAccessor");
            return new Unknown();
        }
        
        var checkAccess = await sender.Send(new CollectionAccessRequest(userId.Value, request.CollectionId), cancellationToken);

        if (checkAccess.IsT1)
        {
            return new Forbidden();
        }

        var result = await repository.DeleteDocumentsAsync(request.CollectionId, request.DocumentIds);

        return result.Match(success => success, ToError);

        OneOf<Success, Unknown, Forbidden, Unreachable> ToError(Unreachable<string> error)
        {
            logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(IFileRepository), error.Value);
            return new();
        }
    }
}