using Badger.Clock;
using Badger.OneOf.Extensions;
using Badger.OneOf.Types;
using Col.Contracts.V1;
using Doc.Contracts.V1;
using Doc.Domain.V1.DataAccess;
using Idn.Contracts.V1;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf.Types;
using OneOf;

namespace Doc.Domain.V1.Handlers;

internal sealed class RegisterDocumentHandler(
    IDocumentRepository documentRepository,
    IMediator mediator,
    IUserIdAccessor userIdAccessor,
    IClock clock,
    ILogger<RegisterDocumentHandler> logger)
    : IRequestHandler<RegisterDocumentRequest, OneOf<Success, Unknown, NotFound, Conflict, Unreachable>>
{
    public async Task<OneOf<Success, Unknown, NotFound, Conflict, Unreachable>> Handle(RegisterDocumentRequest request, CancellationToken cancellationToken)
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
            return new NotFound();
        }

        var document = new Document(request.DocumentId, request.Name, request.Icon, GetNextVersion(request.ExpectedVersion), clock.Now);

        var result = await documentRepository.RegisterDocumentAsync(request.CollectionId, document, request.files);

        return result.Match(success => success, OneOf<Success, Unknown, NotFound, Conflict, Unreachable>.FromT3, ToError);

        OneOf<Success, Unknown, NotFound, Conflict, Unreachable> ToError(Unreachable<string> error)
        {
            logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(IDocumentRepository), error.Value);
            return new Unreachable();
        }

        long GetNextVersion(long? version)
        {
            if (!version.HasValue)
            {
                return 0;
            }

            return version.Value + 1;
        }
    }
}