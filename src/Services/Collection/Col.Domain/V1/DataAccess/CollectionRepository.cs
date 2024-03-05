using Badger.Collections.Extensions;
using Badger.Service.Error;
using Badger.Sql.Abstractions;
using Badger.Sql.Abstractions.Extensions;
using Badger.Sql.Error;
using Col.Contracts.V1;
using Dapper;
using Idn.Contracts.V1;
using OneOf;
using OneOf.Types;

namespace Col.Domain.V1.DataAccess;

public sealed class CollectionRepository(IDbConnectionFactory connectionFactory, SqlQueries queries) : ICollectionRepository
{
    public Task<OneOf<IReadOnlyList<Collection>, UnreachableDatabaseError>> ListAsync(UserId userId) =>
        connectionFactory.TryAsync(async connection =>
        {
            var sqlQuery = queries.GetCollectionsQuery(userId);

            var entities = await connection.QueryAsync<CollectionEntity>(sqlQuery);

            return entities.Select(entity => new Collection(
                    new CollectionId(entity.Id),
                    entity.Name,
                    entity.Icon,
                    Enum.Parse<EncryptionSide>(entity.EncryptSide),
                    entity.Version,
                    DateTimeOffset.FromUnixTimeMilliseconds(entity.AddedAtTimespan)))
                .ToReadOnlyList();
        }, ToUnreachableError);

    public Task<OneOf<Success, UnreachableDatabaseError>> RegisterAsync(
        UserId userId,
        CollectionId id,
        string name,
        string icon,
        EncryptionSide encryptionSide,
        DateTimeOffset addedAt,
        int version) => connectionFactory.TryAsync(async (connection, transaction) =>
    {
        var createCollectionQuery = queries.RegisterCollectionQuery(id, name, icon, encryptionSide, version, addedAt);

        await connection.ExecuteAsync(createCollectionQuery, transaction: transaction);

        var createUserCollectionQuery = queries.RegisterUserCollectionQuery(userId, id);

        await connection.ExecuteAsync(createUserCollectionQuery, transaction: transaction);

        return new Success();
    }, ToUnreachableError);

    public Task<OneOf<Success, NotFoundDatabaseError, UnreachableDatabaseError>> CheckExistingAsync(CollectionId collectionId) =>
        connectionFactory.TryAsync(async connection =>
        {
            var result = await connection.QuerySingleAsync<bool>(queries.CheckCollectionExistingQuery(collectionId));

            return result
                ? OneOf<Success, NotFoundDatabaseError>.FromT0(new Success())
                : OneOf<Success, NotFoundDatabaseError>.FromT1(new NotFoundDatabaseError());
        }, ToUnreachableError);

    private static UnreachableDatabaseError ToUnreachableError(Exception exception) => new(exception.Message);

    private sealed record CollectionEntity(Guid Id, string Name, string Icon, string EncryptSide, int Version, long AddedAtTimespan);
}
