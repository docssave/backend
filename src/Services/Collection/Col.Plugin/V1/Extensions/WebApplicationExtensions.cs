using Badger.Plugin.Filters;
using Col.Contracts.V1;
using Col.Plugin.V1.Dtos;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Col.Plugin.V1.Extensions;

public static class WebApplicationExtensions
{
    private const string BaseRoute = "api/v1/collections";

    public static void UseCollection(this WebApplication application) =>
        application.MapGroup(BaseRoute).UseCollectionsEndpoints();
    
    private static void UseCollectionsEndpoints(this IEndpointRouteBuilder group)
    {
        group.MapPut("/{collectionId}", RegisterCollectionAsync).AddEndpointFilter<ValidationFilter<RegisterCollectionDto>>();
        group.MapGet("/", ListCollectionsAsync);
    }

    private static async Task<IResult> RegisterCollectionAsync([FromQuery] CollectionId id, RegisterCollectionDto request, [FromServices] IMediator mediator)
    {
        var response = await mediator.Send(new RegisterCollectionRequest(id, request.Name, request.Icon, request.EncryptionSide, request.Version));

        return response.Match(Results.Ok, Results.BadRequest);
    }

    private static async Task<IResult> ListCollectionsAsync([FromServices] IMediator mediator)
    {
        var response = await mediator.Send(new ListCollectionsRequest());

        return response.Match(Results.Ok, Results.BadRequest);
    }
}