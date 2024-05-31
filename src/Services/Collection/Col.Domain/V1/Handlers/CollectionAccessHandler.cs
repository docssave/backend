using Badger.OneOf.Types;
using Col.Contracts.V1;
using Col.Domain.V1.DataAccess;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace Col.Domain.V1.Handlers;

internal sealed class CollectionAccessHandler(ICollectionRepository repository, ILogger<CollectionAccessHandler> logger)
    : IRequestHandler<CollectionAccessRequest, OneOf<Success, Forbidden, Unreachable>>
{
    public async Task<OneOf<Success, Forbidden, Unreachable>> Handle(CollectionAccessRequest request, CancellationToken cancellationToken)
    {
        var result = await repository.CheckAccessAsync(request.UserId, request.CollectionId);

        return result.Match(
            OneOf<Success, Forbidden, Unreachable>.FromT0,
            OneOf<Success, Forbidden, Unreachable>.FromT1,
            ToError);

        OneOf<Success, Forbidden, Unreachable> ToError(Unreachable<string> error)
        {
            logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(ICollectionRepository), error.Value);
            return new Unreachable();
        }
    }
}