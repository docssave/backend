using Badger.Sql.Error;
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
        
        Error<string> ToError(UnreachableDatabaseError error)
        {
            logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(IDocumentRepository), error.Reason);

            return new Error<string>(error.Reason);
        }
    }
}