using Col.Contracts;
using Dapper;
using Idn.Contracts;
using OneOf;
using OneOf.Types;
using Sql.Abstractions;
using Sql.Abstractions.Errors;
using Sql.Abstractions.Extensions;

namespace Col.DataAccess;

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
        _connectionFactory.TryAsync<OneOf<IReadOnlyList<Collection>, UnreachableError>>(async connection =>
    {
        var sqlQuery = _queries.GetCollectionsQuery(userId);

        var entities = await connection.QueryAsync<CollectionEntity>(sqlQuery);

        return entities.Select(entity => new Collection(
                new CollectionId(entity.Id),
                entity.Name,
                entity.Icon,
                Enum.Parse<EncryptSide>(entity.EncryptSide),
                entity.Version))
            .ToReadonlyList();
    }, ToUnreachableError);

    private OneOf<IReadOnlyList<Collection>, UnreachableError> ToUnreachableError(Exception exception) => new UnreachableError(exception.Message);

    public Task<OneOf<Success, UnreachableError>> RegisterCollectionAsync(
        UserId userId,
        CollectionId id,
        string name,
        string icon,
        EncryptSide encryptSide,
        DateTimeOffset addedAt,
        int version) => _connectionFactory.TryAsync(async (connection, transaction) =>
    {
        var createCollectionQuery = _queries.RegisterCollectionQuery(id, name, icon, encryptSide, version);

        await connection.ExecuteAsync(createCollectionQuery, transaction: transaction);

        var createUserCollectionQuery = _queries.RegisterUserCollectionQuery(userId, id);

        await connection.ExecuteAsync(createUserCollectionQuery, transaction: transaction);

        return new Success();
    }, RepositoryResult<Collection>.Failed);

    private sealed record CollectionEntity(Guid Id, string Name, string Icon, string EncryptSide, int Version);
    
    private static IReadOnlyList<T> ToReadonlyList<T>(this IEnumerable<T> source)
    {
        if (source is IReadOnlyList<T> list)
        {
            return list;
        }

        return source.ToList();
    }
}
