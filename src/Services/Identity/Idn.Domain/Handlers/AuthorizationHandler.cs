using Common;
using Idn.Contracts;
using Idn.Contracts.Events;
using Idn.DataAccess;
using MediatR;

namespace Idn.Domain.Handlers;

internal sealed class AuthorizationHandler : IRequestHandler<AuthorizationRequest, AuthorizationResponse>
{
    private readonly IMediator _mediator;
    private readonly IIdentityRepository _repository;
    private readonly ISourceService _sourceService;
    private readonly ITokenService _tokenService;
    private readonly IEncryptor _encryptor;

    public AuthorizationHandler(IMediator mediator, IIdentityRepository repository, ISourceService sourceService, ITokenService tokenService, IEncryptor encryptor)
    {
        _mediator = mediator;
        _repository = repository;
        _sourceService = sourceService;
        _tokenService = tokenService;
        _encryptor = encryptor;
    }

    public async Task<AuthorizationResponse> Handle(AuthorizationRequest request, CancellationToken cancellationToken)
    {
        var userInfo = await _sourceService.ExtractUserInfoAsync(request.Token);

        var result = await _repository.GetUserAsync(userInfo.Id);

        if (!result.IsSuccess)
        {
            return new AuthorizationResponse(null, new Error(ErrorType.ServerError, result.Exception!.ToString()));
        }

        if (result.Value == null)
        {
            var encryptedEmail = await _encryptor.EncryptAsync(userInfo.Email);
            result = await _repository.RegisterUserAsync(new CreateUser(userInfo.Name, encryptedEmail, AuthorizationSource.Google, userInfo.Id));

            if (result.IsSuccess)
            {
                await _mediator.Publish(new UserCreatedEvent(result.Value.Id, result.Value.RegisteredAt), cancellationToken);
            }
        }

        if (!result.IsSuccess)
        {
            return new AuthorizationResponse(null, new Error(ErrorType.ServerError, result.Exception!.ToString()));
        }

        return new AuthorizationResponse(_tokenService.Create(result.Value!), null);
    }
}