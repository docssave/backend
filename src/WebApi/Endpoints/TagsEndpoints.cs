using Idn.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TagContracts;

namespace WebApi.Endpoints
{
    public static class TagsEndpoints
    {
        private const string BaseRoute = "api/v1/tags";

        public static void MapTagsEndpoints(this WebApplication application)
        {
            application.MapPost($"{BaseRoute}", CreateAsync);
        }

        private static async Task<IActionResult> CreateAsync(TagRequest request, [FromServices] IUserIdAccessor userAccessor, [FromServices] IMediator mediator, Tag.DataAccess.Tag tag)
        {
            var response = await mediator.Send(request);

            return new OkObjectResult(response);
        }
    }
}                                     