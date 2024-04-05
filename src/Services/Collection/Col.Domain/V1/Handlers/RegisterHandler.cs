using Badger.Clock;
using Badger.OneOf.Types;
using Col.Contracts.V1;
using Col.Domain.V1.DataAccess;
using Idn.Contracts.V1;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace Col.Domain.V1.Handlers;

internal sealed class RegisterHandler(
    ICollectionRepository repository,
    IUserIdAccessor userIdAccessor,
    IClock clock,
    ILogger<RegisterHandler> logger)
    : IRequestHandler<RegisterCollectionRequest, OneOf<Collection, Unknown, Conflict, Unreachable>>
{
    public async Task<OneOf<Collection, Unknown, Conflict, Unreachable>> Handle(RegisterCollectionRequest request, CancellationToken cancellationToken)
    {
        var userId = userIdAccessor.UserId;

        if (userId == null)
        {
            logger.LogError("Could not get UserId from IUserIdAccessor");
            return new Unknown();
        }

        var addedAt = clock.Now;
        var expectedVersion = request.ExpectedVersion;
        var nextVersion = GetNextVersion(expectedVersion);

        var result = await repository.RegisterAsync(
            userId.Value,
            request.Id,
            request.Name,
            request.Icon,
            request.EncryptionSide,
            addedAt,
            expectedVersion,
            GetNextVersion(expectedVersion));

        return result.Match(ToCollection, OneOf<Collection, Unknown, Conflict, Unreachable>.FromT2, ToError);

        OneOf<Collection, Unknown, Conflict, Unreachable> ToCollection(Success _)
            => new Collection(request.Id, request.Name, request.Icon, request.EncryptionSide, nextVersion, addedAt);

        OneOf<Collection, Unknown, Conflict, Unreachable> ToError(Unreachable<string> error)
        {
            logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(ICollectionRepository), error.Value);
            return new Unreachable();
        }

        int GetNextVersion(int? version)
        {
            if (!version.HasValue)
            {
                return 0;
            }

            return version.Value + 1;
        }
    }
}