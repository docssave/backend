using System.Net;
using Badger.Plugin.Filters;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Tg.Contracts.V1;
using Tg.Plugin.V1.Dtos;
using Tg.Plugin.V1.Validators;

namespace Tg.Plugin.V1.Extensions;

public static class WebApplicationExtensions
{
    private const string BaseRoute = "api/v1/tags";

    public static void UseTag(this WebApplication application) =>
        application.MapGroup(BaseRoute).UserTagsEndpoints();

    private static void UserTagsEndpoints(this IEndpointRouteBuilder group)
    {
        group.MapPut("/", RegisterTagAsync).AddEndpointFilter<ValidationFilter<RegisterTagDtoValidator>>();
        group.MapGet("/", GetTagsAsync);
    }

    private static async Task<IResult> RegisterTagAsync(RegisterTagDto request, [FromServices] IMediator mediator)
    {
        var response = await mediator.Send(new RegisterTagRequest(request.Value));

        return response.Match(
            Results.Ok,
            _ => Results.StatusCode((int)HttpStatusCode.InternalServerError),
            _ => Results.StatusCode((int)HttpStatusCode.ServiceUnavailable));
    }

    private static async Task<IResult> GetTagsAsync([FromServices] IMediator mediator)
    {
        var response = await mediator.Send(new GetTagsRequest());

        return response.Match(
            Results.Ok,
            _ => Results.StatusCode((int)HttpStatusCode.InternalServerError),
            _ => Results.StatusCode((int)HttpStatusCode.ServiceUnavailable));
    }
}