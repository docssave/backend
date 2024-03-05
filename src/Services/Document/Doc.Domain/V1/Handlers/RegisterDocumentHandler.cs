using Badger.Clock;
using Badger.Service.Error;
using Doc.Contracts.V1;
using Doc.Domain.V1.DataAccess;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf.Types;
using OneOf;

namespace Doc.Domain.V1.Handlers;

internal sealed class RegisterDocumentHandler(IDocumentRepository repository, ILogger<RegisterDocumentHandler> logger, IClock clock)
    : IRequestHandler<RegisterDocumentRequest, OneOf<Success, NotFoundServiceError, UnexpectedServiceError>>
{
    public Task<OneOf<Success, NotFoundServiceError, UnexpectedServiceError>> Handle(RegisterDocumentRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}