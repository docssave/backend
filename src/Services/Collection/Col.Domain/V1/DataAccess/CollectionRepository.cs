using Badger.Collections.Extensions;
using Badger.OneOf.Types;
using Badger.Sql.Abstractions;
using Badger.Sql.Abstractions.Extensions;
using Col.Contracts.V1;
using Dapper;
using Idn.Contracts.V1;
using OneOf;
using OneOf.Types;

namespace Col.Domain.V1.DataAccess;

public sealed class CollectionRepository(IDbConnectionFactory connectionFactory, SqlQueries queries) : ICollectionRepository
{
    public Task<OneOf<IReadOnlyList<Collection>, Unreachable<string>>> ListAsync(UserId userId) =>
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

    public Task<OneOf<Success, Conflict, Unreachable<string>>> RegisterAsync(
        UserId userId,
        CollectionId id,
        string name,
        string icon,
        EncryptionSide encryptionSide,
        DateTimeOffset addedAt,
        int? expectedVersion,
        int nextVersion) => connectionFactory.TryAsync(async (connection, transaction) =>
    {
        var existingVersion = await connection.QuerySingleAsync<int?>(queries.GetCollectionVersionQuery(id), transaction: transaction);

        if (existingVersion.HasValue && existingVersion != expectedVersion)
        {
            return OneOf<Success, Conflict>.FromT1(new Conflict());
        }

        var createCollectionQuery = queries.RegisterCollectionQuery(id, name, icon, encryptionSide, nextVersion, addedAt);

        await connection.ExecuteAsync(createCollectionQuery, transaction: transaction);

        var createUserCollectionQuery = queries.RegisterUserCollectionQuery(userId, id);

        await connection.ExecuteAsync(createUserCollectionQuery, transaction: transaction);

        return OneOf<Success, Conflict>.FromT0(new Success());
    }, ToUnreachableError);

    public Task<OneOf<Success, NotFound, Unreachable<string>>> CheckExistingAsync(CollectionId collectionId) =>
        connectionFactory.TryAsync(async connection =>
        {
            var result = await connection.QuerySingleAsync<bool>(queries.CheckCollectionExistingQuery(collectionId));

            return result
                ? OneOf<Success, NotFound>.FromT0(new Success())
                : OneOf<Success, NotFound>.FromT1(new NotFound());
        }, ToUnreachableError);

    public Task<OneOf<Success, Forbidden, Unreachable<string>>> CheckAccessAsync(UserId userId, CollectionId collectionId) =>
        connectionFactory.TryAsync(async connection =>
        {
            var result = await connection.QuerySingleAsync<bool>(queries.GetCollectionAccessQuery(userId, collectionId));

            return result
                ? OneOf<Success, Forbidden>.FromT0(new Success())
                : OneOf<Success, Forbidden>.FromT1(new Forbidden());
        }, ToUnreachableError);

    public Task<OneOf<Success<CollectionId[]>, Unreachable<string>>> DeleteCollectionsAsync(UserId userId, CollectionId[] collectionIds) =>
        connectionFactory.TryAsync(async connection =>
        {
            var query = queries.GetDeleteCollectionsQuery(userId, collectionIds);

            var ids = await connection.QueryAsync<Guid>(query);

            return new Success<CollectionId[]>(ids.Select(id => new CollectionId(id)).ToArray());
        }, ToUnreachableError);

    private static Unreachable<string> ToUnreachableError(Exception exception) => new(exception.Message);

    private sealed record CollectionEntity(Guid Id, string Name, string Icon, string EncryptSide, int Version, long AddedAtTimespan);
}
