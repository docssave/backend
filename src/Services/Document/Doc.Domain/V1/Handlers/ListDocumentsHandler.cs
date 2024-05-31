using Badger.OneOf.Types;
using Doc.Contracts.V1;
using Doc.Domain.V1.DataAccess;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace Doc.Domain.V1.Handlers;

internal sealed class ListDocumentsHandler(IDocumentRepository repository, ILogger<ListDocumentsHandler> logger)
    : IRequestHandler<ListDocumentsRequest, OneOf<IReadOnlyList<Document>, Error<string>>>
{
    public async Task<OneOf<IReadOnlyList<Document>, Error<string>>> Handle(ListDocumentsRequest request, CancellationToken cancellationToken)
    {
        var result = await repository.ListDocumentsAsync(request.CollectionId);

        return result.MapT1(ToError);

        Error<string> ToError(Unreachable<string> error)
        {
            logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(IDocumentRepository), error.Value);

            return new Error<string>(error.Value);
        }
    }
}