using Badger.Plugin.Filters;
using Idn.Contracts;
using Idn.Contracts.V1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Idn.Plugin.V1.Extensions;

public static class WebApplicationExtensions
{
    private const string BaseRoute = "api/v1/users";
    
    public static void UseIdentity(this WebApplication application)
    {
        application.UseMiddleware<UserIdAccessorMiddleware>();
        
        application.MapPut($"{BaseRoute}/authorization", AuthorizationAsync)
            .AllowAnonymous()
            .AddEndpointFilter<ValidationFilter<AuthorizationRequest>>();
    }

    private static async Task<IResult> AuthorizationAsync(AuthorizationRequest request, [FromServices] IMediator mediator)
    {
        var response = await mediator.Send(request);
    
        return response.Match(Results.Ok, Results.BadRequest);
    }
}