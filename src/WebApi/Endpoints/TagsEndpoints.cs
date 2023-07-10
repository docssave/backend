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
            application.MapPut($"{BaseRoute}", CreateTagAsync);
        }

        private static async Task<IResult> CreateTagAsync(
            CreateTagRequest request,
            [FromServices] IUserIdAccessor userAccessor,
            [FromServices] IMediator mediator)
        {
            var response = await mediator.Send(request);

            return Results.Ok(response);
        }
    }
}                                     