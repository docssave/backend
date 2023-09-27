using Col.Contracts.V1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Col.Plugin.V1.Extensions;

public static class WebApplicationExtensions
{
    private const string BaseRoute = "api/v1/collection";

    public static void UseCollection(this WebApplication application)
    {
        application.MapPut(BaseRoute, RegisterCollectionAsync);
    }

    private static async Task<IResult> RegisterCollectionAsync(RegisterCollectionRequest request, [FromServices] IMediator mediator)
    {
        var response = await mediator.Send(request);

        return response.Match(Results.Ok, Results.BadRequest);
    }
}