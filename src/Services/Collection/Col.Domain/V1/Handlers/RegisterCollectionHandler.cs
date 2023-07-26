using Col.Contracts.V1;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Col.Domain.V1.Handlers;

internal sealed class RegisterCollectionHandler : IRequestHandler<RegisterCollectionRequest, OneOf<Collection, Error<string>>>
{
    public Task<OneOf<Collection, Error<string>>> Handle(RegisterCollectionRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}