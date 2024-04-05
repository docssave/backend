using Badger.OneOf.Types;
using Col.Contracts.V1;
using Col.Domain.V1.DataAccess;
using Idn.Contracts.V1;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace Col.Domain.V1.Handlers;

internal sealed class ListHandler(ICollectionRepository repository, IUserIdAccessor userIdAccessor, ILogger<ListHandler> logger)
    : IRequestHandler<ListCollectionsRequest, OneOf<IReadOnlyList<Collection>, Unknown, Unreachable>>
{
    public async Task<OneOf<IReadOnlyList<Collection>, Unknown, Unreachable>> Handle(ListCollectionsRequest request, CancellationToken cancellationToken)
    {
        var userId = userIdAccessor.UserId;

        if (userId == null)
        {
            logger.LogError("Could not get UserId from IUserIdAccessor");
            return new Unknown();
        }

        var result = await repository.ListAsync(userId.Value);

        return result.Match(ToSuccess, ToError);

        OneOf<IReadOnlyList<Collection>, Unknown, Unreachable> ToSuccess(IReadOnlyList<Collection> list)
        {
            return OneOf<IReadOnlyList<Collection>, Unknown, Unreachable>.FromT0(list);
        }

        OneOf<IReadOnlyList<Collection>, Unknown, Unreachable> ToError(Unreachable<string> error)
        {
            logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(ICollectionRepository), error.Value);
            return new Unreachable();
        }
    }
}