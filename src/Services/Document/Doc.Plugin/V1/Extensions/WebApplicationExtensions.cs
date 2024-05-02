using Badger.AspNetCore.Extensions;
using Badger.Plugin.Filters;
using Col.Contracts.V1;
using Doc.Contracts.V1;
using Doc.Plugin.V1.Dtos;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using File = Doc.Contracts.V1.File;

namespace Doc.Plugin.V1.Extensions;

public static class WebApplicationExtensions
{
    private const string BaseRoute = "api/v1";

    public static void UseDocument(this WebApplication application) =>
        application.MapGroup(BaseRoute).UseDocumentsEndpoints();

    private static void UseDocumentsEndpoints(this IEndpointRouteBuilder group)
    {
        group.MapPut("/{collectionId:guid}/documents/{documentId:guid}", RegisterDocumentsAsync).AddEndpointFilter<ValidationFilter<RegisterDocumentDto>>();
        group.MapGet("/{collectionId:guid}/documents", ListDocumentsAsync);
    }

    private static async Task<IResult> RegisterDocumentsAsync(
        [FromRoute] Guid collectionId,
        [FromRoute] Guid documentId,
        RegisterDocumentDto request,
        IFormFileCollection fileCollection,
        [FromServices] IMediator mediator)
    {
        var files = fileCollection.Select(file => new File(FileId.New(), [], file.ContentType)).ToArray(); // TODO: read content from each file
        var registerDocumentRequest = new RegisterDocumentRequest(
            new CollectionId(collectionId),
            new DocumentId(documentId),
            request.Name,
            request.Icon,
            request.ExpectedVersion,
            files);

        var result = await mediator.Send(registerDocumentRequest);

        return result.Match(
            Results.Ok,
            Results.Extensions.Unknown,
            Results.NotFound,
            Results.Extensions.RetryLate);
    }

    private static async Task<IResult> ListDocumentsAsync([FromRoute] Guid collectionId, [FromServices] IMediator mediator)
    {
        var result = await mediator.Send(new ListDocumentsRequest(collectionId));

        return result.Match(Results.Ok, Results.BadRequest);
    }
}