using Badger.OneOf.Types;
using Idn.Contracts.V1;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf.Types;
using Tg.Contracts.V1;
using OneOf;
using Tg.Domain.V1.DataAccess;

namespace Tg.Domain.V1.Handlers;

internal sealed class RegisterTagHandler(ITagRepository repository, IUserIdAccessor userIdAccessor, ILogger<RegisterTagHandler> logger) :
    IRequestHandler<RegisterTagRequest, OneOf<Success, Unknown, Unreachable>>
{
    public Task<OneOf<Success, Unknown, Unreachable>> Handle(RegisterTagRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}