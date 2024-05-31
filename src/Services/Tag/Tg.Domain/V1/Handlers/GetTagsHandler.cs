using Badger.OneOf.Types;
using Idn.Contracts.V1;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf.Types;
using Tg.Contracts.V1;
using Tg.Domain.V1.DataAccess;
using OneOf;

namespace Tg.Domain.V1.Handlers;

internal sealed class GetTagsHandler(ITagRepository repository, IUserIdAccessor userIdAccessor, ILogger<GetTagsHandler> logger)
    : IRequestHandler<GetTagsRequest, OneOf<IReadOnlyCollection<Tag>, Unknown, Unreachable>>
{
    public async Task<OneOf<IReadOnlyCollection<Tag>, Unknown, Unreachable>> Handle(GetTagsRequest request, CancellationToken cancellationToken)
    {
        var userId = userIdAccessor.UserId;

        if (userId == null)
        {
            logger.LogError("Could not get UserId from IUserIdAccessor");

            return new Unknown();
        }

        var result = await repository.GetAsync(userId.Value);

        return result.Match(ToSuccess, ToError);

        OneOf<IReadOnlyCollection<Tag>, Unknown, Unreachable> ToSuccess(IReadOnlyCollection<Tag> tags) => OneOf<IReadOnlyCollection<Tag>, Unknown, Unreachable>.FromT0(tags);

        OneOf<IReadOnlyCollection<Tag>, Unknown, Unreachable> ToError(Unreachable<string> error)
        {
            logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(ITagRepository), error.Value);
            return new Unreachable();
        }
    }
}