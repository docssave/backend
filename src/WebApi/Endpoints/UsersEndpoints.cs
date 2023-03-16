using Idn.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Endpoints;

public static class UsersEndpoints
{
    private const string BaseRoute = "api/v1/users";
    
    public static void MapUserEndpoints(this WebApplication application)
    {
        application.MapPut($"{BaseRoute}/authorization", AuthorizationAsync);
    }

    private static async Task<IResult> AuthorizationAsync(string token, Source source, [FromServices] IMediator mediator)
    {
        var response = await mediator.Send(new AuthorizationRequest(token, source));
        
        return Results.Ok(response.Token);
    }
}