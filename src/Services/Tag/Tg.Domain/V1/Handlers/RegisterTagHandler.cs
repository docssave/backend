using Badger.OneOf.Types;
using Badger.Sql.Abstractions.Errors;
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
    public async Task<OneOf<Success, Unknown, Unreachable>> Handle(RegisterTagRequest request, CancellationToken cancellationToken)
    {
        var userId = userIdAccessor.UserId;

        if (userId == null)
        {
            logger.LogError("Could not get UserId from IUserIdAccessor");

            return new Unknown();
        }

        var result = await repository.RegisterAsync(userId.Value, request.Value);

        return result.Match(ToSuccess, ToError);

        OneOf<Success, Unknown, Unreachable> ToSuccess(Success success) => OneOf<Success, Unknown, Unreachable>.FromT0(success);

        OneOf<Success, Unknown, Unreachable> ToError(UnreachableError error)
        {
            logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(ITagRepository), error.Reason);

            return new Unreachable();
        }
    }
}