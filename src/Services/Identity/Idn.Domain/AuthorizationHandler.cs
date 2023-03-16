using Idn.Contracts;
using MediatR;

namespace Idn.Domain;

public sealed class AuthorizationHandler : IRequestHandler<AuthorizationRequest, AuthorizationResponse> 
{
    public Task<AuthorizationResponse> Handle(AuthorizationRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new AuthorizationResponse(Guid.NewGuid().ToString()));
    }
}