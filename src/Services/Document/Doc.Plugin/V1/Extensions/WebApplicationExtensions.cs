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
        group.MapPut("/{collectionId}/documents/{documentId}", RegisterDocumentAsync).AddEndpointFilter<ValidationFilter<RegisterDocumentDto>>();
        group.MapGet("/{collectionId}/documents", ListDocumentsAsync);
        group.MapDelete("/{collectionId}/documents/{documentId}/files", DeleteFilesAsync);
        group.MapDelete("/{collectionId}/documents", DeleteDocumentsAsync);
    }

    private static async Task<IResult> RegisterDocumentAsync(
        [FromRoute] CollectionId collectionId,
        [FromRoute] DocumentId documentId,
        RegisterDocumentDto request,
        IFormFileCollection fileCollection,
        [FromServices] IMediator mediator)
    {
        var files = await Task.WhenAll(fileCollection.Select(LoadFileAsync));

        var registerDocumentRequest = new RegisterDocumentRequest(
            collectionId,
            documentId,
            request.Name,
            request.Icon,
            request.ExpectedVersion,
            files);

        var result = await mediator.Send(registerDocumentRequest);

        return result.Match(
            Results.Ok,
            Results.Extensions.Unknown,
            Results.NotFound,
            Results.Conflict,
            Results.Extensions.RetryLate);

        static async Task<File> LoadFileAsync(IFormFile file)
        {
            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return new File(FileId.New(), memoryStream.ToArray(), file.ContentType);
        }
    }

    private static async Task<IResult> ListDocumentsAsync([FromRoute] CollectionId collectionId, [FromServices] IMediator mediator)
    {
        var result = await mediator.Send(new ListDocumentsRequest(collectionId));

        return result.Match(Results.Ok, Results.BadRequest);
    }

    private static async Task<IResult> DeleteFilesAsync(
        [FromRoute] CollectionId collectionId,
        [FromRoute] DocumentId documentId,
        [FromQuery(Name = "fileId")] FileId[] fileIds,
        [FromServices] IMediator mediator)
    {
        var result = await mediator.Send(new DeleteFilesRequest(collectionId, documentId, fileIds));

        return result.Match(
            Results.Ok,
            Results.Extensions.Unknown,
            _ => Results.Forbid(),
            Results.Extensions.RetryLate);
    }

    private static async Task<IResult> DeleteDocumentsAsync(
        [FromRoute] CollectionId collectionId,
        [FromQuery(Name = "documentId")] DocumentId[] documentIds,
        [FromServices] IMediator mediator)
    {
        var result = await mediator.Send(new DeleteDocumentsRequest(collectionId, documentIds));

        return result.Match(
            Results.Ok,
            Results.Extensions.Unknown,
            _ => Results.Forbid(),
            Results.Extensions.RetryLate);
    }
}