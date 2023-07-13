using Idn.Contracts;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WebApi.Filters;

namespace WebApi.Endpoints;

public static class UsersEndpoints
{
    private const string BaseRoute = "api/v1/users";
    
    public static void MapUsersEndpoints(this IEndpointRouteBuilder application)
    {
        application.MapPut($"{BaseRoute}/authorization", AuthorizationAsync)
            .AllowAnonymous()
            .AddEndpointFilter<ValidatorFilter<AuthorizationRequest>>();
    }

    private static async Task<IResult> AuthorizationAsync(AuthorizationRequest request, [FromServices] IMediator mediator)
    {
        var response = await mediator.Send(request);
    
        return response.Match(Results.Ok, Results.BadRequest);
    }
}