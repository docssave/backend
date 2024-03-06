using Badger.Clock;
using Badger.OneOf.Types;
using Col.Contracts.V1;
using Doc.Contracts.V1;
using Doc.Domain.V1.DataAccess;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf.Types;
using OneOf;

namespace Doc.Domain.V1.Handlers;

internal sealed class RegisterDocumentHandler(IDocumentRepository repository, ILogger<RegisterDocumentHandler> logger, IClock clock)
    : IRequestHandler<RegisterDocumentRequest, OneOf<Success, NotFound<CollectionId>, Unreachable>>
{
    public Task<OneOf<Success, NotFound<CollectionId>, Unreachable>> Handle(RegisterDocumentRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}