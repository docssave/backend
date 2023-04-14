using Idn.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Endpoints;

public static class TagsEndpoints
{
    private const string BaseRoute = "api/v1/tags";

    public static void MapTagsEndpoints(this WebApplication application)
    {
        application.MapPut($"{BaseRoute}", CreateAsync);
    }

    private static async Task<IResult> CreateAsync([FromServices] IUserIdAccessor userIdAccessor)
    {
        return Results.Ok();
    }
}