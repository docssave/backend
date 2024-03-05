using Col.Contracts.V1;
using Doc.Contracts.V1;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Doc.Plugin.V1.Extensions;

public static class WebApplicationExtensions
{
    private const string BaseRoute = "api/v1";

    public static void UseDocument(this WebApplication application) =>
        application.MapGroup(BaseRoute).UseDocumentsEndpoints();

    private static void UseDocumentsEndpoints(this IEndpointRouteBuilder group)
    {
        group.MapPut("/{collectionId}/documents/{documentId}", RegisterDocumentsAsync);
        group.MapGet("/{collectionId}/documents", ListDocumentsAsync);
    }

    private static Task<IResult> RegisterDocumentsAsync([FromRoute] CollectionId collectionId, IFormFileCollection files, [FromServices] IMediator mediator)
    {
        return Task.FromResult(Results.Empty);
    }

    private static async Task<IResult> ListDocumentsAsync([FromRoute] CollectionId collectionId, [FromServices] IMediator mediator)
    {
        var result = await mediator.Send(new ListDocumentsRequest(collectionId));

        return result.Match(Results.Ok, Results.BadRequest);
    }
}