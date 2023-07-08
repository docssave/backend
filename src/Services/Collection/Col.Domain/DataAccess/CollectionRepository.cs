using Col.Contracts;
using Collections.Extensions;
using Dapper;
using Idn.Contracts;
using OneOf;
using OneOf.Types;
using Sql.Abstractions;
using Sql.Abstractions.Errors;
using Sql.Abstractions.Extensions;

namespace Col.Domain.DataAccess;

public sealed class CollectionRepository : ICollectionRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly SqlQueries _queries;

    public CollectionRepository(IDbConnectionFactory connectionFactory, SqlQueries queries)
    {
        _connectionFactory = connectionFactory;
        _queries = queries;
    }

    public Task<OneOf<IReadOnlyList<Collection>, UnreachableError>> ListAsync(UserId userId) =>
        _connectionFactory.TryAsync(async connection =>
        {
            var sqlQuery = _queries.GetCollectionsQuery(userId);

            var entities = await connection.QueryAsync<CollectionEntity>(sqlQuery);

            return entities.Select(entity => new Collection(
                    new CollectionId(entity.Id),
                    entity.Name,
                    entity.Icon,
                    Enum.Parse<EncryptionSide>(entity.EncryptSide),
                    entity.Version,
                    DateTimeOffset.FromUnixTimeMilliseconds(entity.AddedAtTimespan)
                    ))
                .ToReadonlyList();
        }, ToUnreachableError);

    public Task<OneOf<Success, UnreachableError>> RegisterAsync(
        UserId userId,
        CollectionId id,
        string name,
        string icon,
        EncryptionSide encryptionSide,
        DateTimeOffset addedAt,
        int version) => _connectionFactory.TryAsync(async (connection, transaction) =>
    {
        var createCollectionQuery = _queries.RegisterCollectionQuery(id, name, icon, encryptionSide, version, addedAt);

        await connection.ExecuteAsync(createCollectionQuery, transaction: transaction);

        var createUserCollectionQuery = _queries.RegisterUserCollectionQuery(userId, id);

        await connection.ExecuteAsync(createUserCollectionQuery, transaction: transaction);

        return new Success();
    }, ToUnreachableError);
    
    private static UnreachableError ToUnreachableError(Exception exception) => new(exception.Message);

    private sealed record CollectionEntity(Guid Id, string Name, string Icon, string EncryptSide, int Version, long AddedAtTimespan);
}
