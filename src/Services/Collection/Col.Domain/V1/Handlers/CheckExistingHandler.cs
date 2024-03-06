using Badger.OneOf.Types;
using Col.Contracts.V1;
using Col.Domain.V1.DataAccess;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Col.Domain.V1.Handlers;

internal sealed class CheckExistingHandler(ICollectionRepository repository)
    : IRequestHandler<CheckCollectionExistingRequest, OneOf<Success, NotFound, Unreachable>>
{
    public Task<OneOf<Success, NotFound, Unreachable>> Handle(CheckCollectionExistingRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}