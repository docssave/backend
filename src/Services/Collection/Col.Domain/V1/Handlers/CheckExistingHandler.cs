using Badger.Service.Error;
using Col.Contracts.V1;
using Col.Domain.V1.DataAccess;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Col.Domain.V1.Handlers;

internal sealed class CheckExistingHandler(ICollectionRepository repository)
    : IRequestHandler<CheckCollectionExistingRequest, OneOf<Success, NotFoundServiceError, UnexpectedServiceError>>
{
    public Task<OneOf<Success, NotFoundServiceError, UnexpectedServiceError>> Handle(CheckCollectionExistingRequest request, CancellationToken cancellationToken)
    {
        
    }
}