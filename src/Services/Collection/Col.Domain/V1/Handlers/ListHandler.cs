using Badger.Sql.Error;
using Col.Contracts.V1;
using Col.Domain.V1.DataAccess;
using Idn.Contracts.V1;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf.Types;
using OneOf;

namespace Col.Domain.V1.Handlers;

internal sealed class ListHandler(ICollectionRepository repository, IUserIdAccessor userIdAccessor, ILogger<ListHandler> logger)
    : IRequestHandler<ListCollectionsRequest, OneOf<IReadOnlyList<Collection>, Error<string>>>
{
    public async Task<OneOf<IReadOnlyList<Collection>, Error<string>>> Handle(ListCollectionsRequest request, CancellationToken cancellationToken)
    {
        var userId = userIdAccessor.UserId;

        if (userId == null)
        {
            logger.LogError("Could not get UserId from IUserIdAccessor");
            
            return new Error<string>("Could not continue the operation.");
        }

        var result = await repository.ListAsync(userId.Value);

        return result.MapT1(ToError);

        Error<string> ToError(UnreachableDatabaseError error)
        {
            logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(ICollectionRepository), error.Reason);

            return new Error<string>(error.Reason);
        }
    }
}