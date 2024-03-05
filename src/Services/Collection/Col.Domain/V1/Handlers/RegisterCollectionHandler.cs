using Badger.Clock;
using Badger.Sql.Error;
using Col.Contracts.V1;
using Col.Domain.V1.DataAccess;
using Idn.Contracts.V1;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace Col.Domain.V1.Handlers;

internal sealed class RegisterCollectionHandler(
    ICollectionRepository repository,
    IUserIdAccessor userIdAccessor,
    IClock clock,
    ILogger<RegisterCollectionHandler> logger)
    : IRequestHandler<RegisterCollectionRequest, OneOf<Collection, Error<string>>>
{
    public async Task<OneOf<Collection, Error<string>>> Handle(RegisterCollectionRequest request, CancellationToken cancellationToken)
    {
        var userId = userIdAccessor.UserId;

        if (userId == null)
        {
            logger.LogError("Could not get UserId from IUserIdAccessor");
            
            return new Error<string>("Could not continue the operation.");
        }

        var addedAt = clock.Now;
        var version = request.Version ?? 0;

        var result = await repository.RegisterAsync(userId.Value, request.Id, request.Name, request.Icon, request.EncryptionSide, addedAt, version);

        return result.Match(ToCollection, ToError);

        OneOf<Collection, Error<string>> ToCollection(Success _) => new Collection(request.Id, request.Name, request.Icon, request.EncryptionSide, version, addedAt);

        OneOf<Collection, Error<string>> ToError(UnreachableDatabaseError error)
        {
            logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(ICollectionRepository), error.Reason);

            return new Error<string>(error.Reason);
        }
    }
}