using Badger.Clock;
using Badger.Sql.Abstractions.Errors;
using Col.Contracts.V1;
using Col.Domain.V1.DataAccess;
using Idn.Contracts.V1;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace Col.Domain.V1.Handlers;

internal sealed class RegisterCollectionHandler : IRequestHandler<RegisterCollectionRequest, OneOf<Collection, Error<string>>>
{
    private readonly ICollectionRepository _repository;
    private readonly IUserIdAccessor _userIdAccessor;
    private readonly IClock _clock;
    private readonly ILogger<RegisterCollectionHandler> _logger;

    public RegisterCollectionHandler(ICollectionRepository repository,
        IUserIdAccessor userIdAccessor,
        IClock clock,
        ILogger<RegisterCollectionHandler> logger)
    {
        _repository = repository;
        _userIdAccessor = userIdAccessor;
        _clock = clock;
        _logger = logger;
    }
    
    public async Task<OneOf<Collection, Error<string>>> Handle(RegisterCollectionRequest request, CancellationToken cancellationToken)
    {
        var userId = _userIdAccessor.UserId;

        if (userId == null)
        {
            _logger.LogError("Could not get UserId from IUserIdAccessor");
            
            return new Error<string>("Could not continue the operation.");
        }

        var addedAt = _clock.Now;
        var version = request.Version ?? 0;

        var result = await _repository.RegisterAsync(userId.Value, request.Id, request.Name, request.Icon, request.EncryptionSide, addedAt, version);

        return result.Match(ToCollection, ToError);

        OneOf<Collection, Error<string>> ToCollection(Success _) => new Collection(request.Id, request.Name, request.Icon, request.EncryptionSide, version, addedAt);

        OneOf<Collection, Error<string>> ToError(UnreachableError error)
        {
            _logger.LogError("Could not reach `{Repository}` with the reason: {Reason}", nameof(ICollectionRepository), error.Reason);

            return new Error<string>(error.Reason);
        }
    }
}