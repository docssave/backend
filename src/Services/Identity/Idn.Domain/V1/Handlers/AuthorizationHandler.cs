using Badger.Clock;
using Badger.OneOf.Extensions;
using Badger.Sql.Abstractions.Errors;
using Idn.Contracts.V1;
using Idn.Contracts.V1.Events;
using Idn.Domain.V1.DataAccess;
using Idn.Domain.V1.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace Idn.Domain.V1.Handlers;

internal sealed class AuthorizationHandler(
    IMediator mediator,
    IIdentityRepository repository,
    ISourceService sourceService,
    ITokenService tokenService,
    IEncryptor encryptor,
    IClock clock,
    ILogger<AuthorizationHandler> logger)
    : IRequestHandler<AuthorizationRequest, OneOf<AuthorizationResponse, Error<string>>>
{
    public async Task<OneOf<AuthorizationResponse, Error<string>>> Handle(AuthorizationRequest request, CancellationToken cancellationToken)
    {
        var userInfoResult = await sourceService.ExtractUserInfoAsync(request.Token);

        if (userInfoResult.IsT1)
        {
            logger.LogError("Could not extract user info from token; returned error is `{Error}`", userInfoResult.AsT1);

            return userInfoResult.AsT1;
        }

        var sourceUserInfo = userInfoResult.AsT0;

        var getUserResult = await repository.GetUserAsync(sourceUserInfo.Id);

        return await getUserResult.Match(ToToken, RegisterUser, ToError);

        OneOf<AuthorizationResponse, Error<string>> ToToken(User user) => new AuthorizationResponse(tokenService.Create(user));

        async Task<OneOf<AuthorizationResponse, Error<string>>> RegisterUser(NotFound _)
        {
            var encryptedEmail = await encryptor.EncryptAsync(sourceUserInfo.Email);
            var registerUserResult = await repository.RegisterUserAsync(
                sourceUserInfo.Id,
                sourceUserInfo.Name,
                encryptedEmail,
                request.Source ?? AuthorizationSource.Google,
                clock.Now);

            if (registerUserResult.IsT0)
            {
                var registeredUser = registerUserResult.AsT0;
                await mediator.Publish(new UserRegisteredEvent(registeredUser.Id, registeredUser.RegisteredAt), cancellationToken);
            }

            return registerUserResult.Match(ToToken, ToError);
        }

        OneOf<AuthorizationResponse, Error<string>> ToError(UnreachableError unreachableError)
        {
            logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(IIdentityRepository), unreachableError.Reason);
            
            return new Error<string>(unreachableError.Reason);
        }
    }
}