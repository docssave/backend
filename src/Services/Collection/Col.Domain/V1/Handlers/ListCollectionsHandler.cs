using Badger.Sql.Abstractions.Errors;
using Col.Contracts.V1;
using Col.Domain.V1.DataAccess;
using Idn.Contracts.V1;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf.Types;
using OneOf;

namespace Col.Domain.V1.Handlers;

internal sealed class ListCollectionsHandler : IRequestHandler<ListCollectionsRequest, OneOf<IReadOnlyList<Collection>, Error<string>>>
{
    private readonly ICollectionRepository _repository;
    private readonly IUserIdAccessor _userIdAccessor;
    private readonly ILogger<ListCollectionsHandler> _logger;

    public ListCollectionsHandler(ICollectionRepository repository, IUserIdAccessor userIdAccessor, ILogger<ListCollectionsHandler> logger)
    {
        _repository = repository;
        _userIdAccessor = userIdAccessor;
        _logger = logger;
    }

    public async Task<OneOf<IReadOnlyList<Collection>, Error<string>>> Handle(ListCollectionsRequest request, CancellationToken cancellationToken)
    {
        var userId = _userIdAccessor.UserId;

        if (userId == null)
        {
            _logger.LogError("Could not get UserId from IUserIdAccessor");
            
            return new Error<string>("Could not continue the operation.");
        }

        var result = await _repository.ListAsync(userId.Value);

        return result.MapT1(ToError);

        Error<string> ToError(UnreachableError error)
        {
            _logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(ICollectionRepository), error.Reason);

            return new Error<string>(error.Reason);
        }
    }
}