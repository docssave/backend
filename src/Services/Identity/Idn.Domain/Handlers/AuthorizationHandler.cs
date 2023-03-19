using Idn.Contracts;
using Idn.DataAccess;
using MediatR;

namespace Idn.Domain;

internal sealed class AuthorizationHandler : IRequestHandler<AuthorizationRequest, AuthorizationResponse>
{
    private readonly IIdentityRepository _repository;

    public AuthorizationHandler(IIdentityRepository repository) =>
        _repository = repository;

    public Task<AuthorizationResponse> Handle(AuthorizationRequest request, CancellationToken cancellationToken)
    {
        /*
         * [] - extract source user id
         * [] - check if user exists
         *  [] - yes: generate token
         *  [] - no: add user into database, raise UserCreatedEvent, generate token
         */
        
        return Task.FromResult(new AuthorizationResponse(Guid.NewGuid().ToString()));
    }
}