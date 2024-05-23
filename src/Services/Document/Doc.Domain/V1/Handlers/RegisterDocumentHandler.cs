using Badger.Clock;
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
    public async Task<OneOf<Success, Unknown, NotFound, Unreachable>> Handle(RegisterDocumentRequest request, CancellationToken cancellationToken)
    {
         var userId = userIdAccessor.UserId;
        
         if (userId == null)
         {
             logger.LogError("Could not get UserId fromIuserIdAccessor");
             return new Unknown();
         }
        
         var checkCollection = await mediator.Send(new CheckCollectionExistingRequest(request.CollectionId), cancellationToken);
        
         if (checkCollection.IsT1)
         {
             return new NotFound();
         }

         var registrationDocument = await documentRepository.RegisterDocumentAsync(
             request.CollectionId,
             request.DocumentId,
             request.Name,
             request.Icon,
             request.ExpectedVersion,
             request.files);
    }
}