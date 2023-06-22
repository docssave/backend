using Col.Contracts;
using Dapper;
using Idn.Contracts;
using SqlServer.Abstraction;
using SqlServer.Abstraction.Extensions;

namespace Col.DataAccess;

public sealed class CollectionRepository : RepositoryBase, ICollectionRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public CollectionRepository(IDbConnectionFactory connectionFactory) => 
        _connectionFactory = connectionFactory;

    public Task<IReadOnlyList<Collection>> ListCollectionsAsync(UserId userId) => _connectionFactory.TryAsync(async connection =>
    {
        var sqlQuery = QueryCompiler.ToSqlQueryString(SqlQueries.GetCollectionsQuery(userId));

        var entities = await connection.QueryAsync<CollectionEntity>(sqlQuery);

        return entities.Select(entity => new Collection(
                new CollectionId(entity.Id),
                entity.Name,
                entity.Icon,
                Enum.Parse<EncryptSide>(entity.EncryptSide),
                entity.Version))
            .ToList();
    }, RepositoryResult<IReadOnlyList<Collection>>.Failed);

    public Task<Collection> RegisterCollectionAsync(CollectionId id, string name, string icon, EncryptSide encryptSide, int version)
    {
        throw new NotImplementedException();
    }

    private sealed record CollectionEntity(Guid Id, string Name, string Icon, string EncryptSide, int Version);
}
