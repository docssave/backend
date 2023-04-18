using Idn.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Endpoints
{
    public static class TagsEndpoints
    {
        private const string BaseRoute = "api/v1/tags";

        public static void MapTagsEndpoints(this WebApplication application)
        {
            application.MapPost($"{BaseRoute}", CreateAsync);
        }

        private static async Task<IActionResult> CreateAsync(TagRequest request, [FromServices] IUserAccessor userAccessor, [FromServices] IMediator mediator)
        {
            var response = await mediator.Send(request);

            return Results.Ok(response.Tag);
        }
    }
}                                     