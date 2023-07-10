using Clock;
using Idn.Contracts;
using Idn.Contracts.Events;
using Idn.Domain.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;
using Sql.Abstractions.Errors;
using DocsSave.OneOf.Extensions;
using Idn.Domain.DataAccess;

namespace Idn.Domain.Handlers;

internal sealed class AuthorizationHandler : IRequestHandler<AuthorizationRequest, OneOf<AuthorizationResponse, Error<string>>>
{
    private readonly IMediator _mediator;
    private readonly IIdentityRepository _repository;
    private readonly ISourceService _sourceService;
    private readonly ITokenService _tokenService;
    private readonly IEncryptor _encryptor;
    private readonly IClock _clock;
    private readonly ILogger<AuthorizationHandler> _logger;

    public AuthorizationHandler(IMediator mediator,
        IIdentityRepository repository,
        ISourceService sourceService,
        ITokenService tokenService,
        IEncryptor encryptor,
        IClock clock,
        ILogger<AuthorizationHandler> logger)
    {
        _mediator = mediator;
        _repository = repository;
        _sourceService = sourceService;
        _tokenService = tokenService;
        _encryptor = encryptor;
        _clock = clock;
        _logger = logger;
    }

    public async Task<OneOf<AuthorizationResponse, Error<string>>> Handle(AuthorizationRequest request, CancellationToken cancellationToken)
    {
        var userInfoResult = await _sourceService.ExtractUserInfoAsync(request.Token);

        if (userInfoResult.IsT1)
        {
            _logger.LogError("Could not extract user info from token; returned error is `{Error}`", userInfoResult.AsT1);

            return userInfoResult.AsT1;
        }

        var sourceUserInfo = userInfoResult.AsT0;

        var getUserResult = await _repository.GetUserAsync(sourceUserInfo.Id);

        return await getUserResult.Match(ToToken, RegisterUser, ToError);

        OneOf<AuthorizationResponse, Error<string>> ToToken(User user) => new AuthorizationResponse(_tokenService.Create(user));

        async Task<OneOf<AuthorizationResponse, Error<string>>> RegisterUser(NotFound _)
        {
            var encryptedEmail = await _encryptor.EncryptAsync(sourceUserInfo.Email);
            var registerUserResult = await _repository.RegisterUserAsync(
                sourceUserInfo.Id,
                sourceUserInfo.Name,
                encryptedEmail,
                request.Source ?? AuthorizationSource.Google,
                _clock.Now);

            if (registerUserResult.IsT0)
            {
                var registeredUser = registerUserResult.AsT0;
                await _mediator.Publish(new UserRegisteredEvent(registeredUser.Id, registeredUser.RegisteredAt), cancellationToken);
            }

            return registerUserResult.Match(ToToken, ToError);
        }

        OneOf<AuthorizationResponse, Error<string>> ToError(UnreachableError unreachableError)
        {
            _logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(IIdentityRepository), unreachableError.Reason);
            
            return new Error<string>(unreachableError.Reason);
        }
    }
}