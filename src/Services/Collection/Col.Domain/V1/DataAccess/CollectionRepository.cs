using Badger.Collections.Extensions;
using Badger.Sql.Abstractions;
using Badger.Sql.Abstractions.Errors;
using Badger.Sql.Abstractions.Extensions;
using Col.Contracts.V1;
using Dapper;
using Idn.Contracts.V1;
using OneOf;
using OneOf.Types;

namespace Col.Domain.V1.DataAccess;

public sealed class CollectionRepository(IDbConnectionFactory connectionFactory, SqlQueries queries) : ICollectionRepository
{
    public Task<OneOf<IReadOnlyList<Collection>, UnreachableError>> ListAsync(UserId userId) =>
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

    public Task<OneOf<Success, UnreachableError>> RegisterAsync(
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
    
    private static UnreachableError ToUnreachableError(Exception exception) => new(exception.Message);

    private sealed record CollectionEntity(Guid Id, string Name, string Icon, string EncryptSide, int Version, long AddedAtTimespan);
}
