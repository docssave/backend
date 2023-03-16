using Idn.Contracts;
using Idn.DataAccess;
using MediatR;

namespace Idn.Domain;

public sealed class AuthorizationHandler : IRequestHandler<AuthorizationRequest, AuthorizationResponse>
{
    private readonly IIdentityRepository _repository;

    public AuthorizationHandler(IIdentityRepository repository) =>
        _repository = repository;

    public Task<AuthorizationResponse> Handle(AuthorizationRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new AuthorizationResponse(Guid.NewGuid().ToString()));
    }
}