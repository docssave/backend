using Idn.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Endpoints;

public static class UsersEndpoints
{
    private const string BaseRoute = "api/v1/users";
    
    public static void MapUsersEndpoints(this WebApplication application)
    {
        application.MapPut($"{BaseRoute}/authorization", AuthorizationAsync).AllowAnonymous();
    }

    private static async Task<IResult> AuthorizationAsync(AuthorizationRequest request, [FromServices] IMediator mediator)
    {
        var response = await mediator.Send(request);

        return response.IsSuccess ? Results.Ok(response.Token) : Results.BadRequest(response.Error!.Message);
    }
}