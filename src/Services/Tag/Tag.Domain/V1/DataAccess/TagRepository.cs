using Badger.Collections.Extensions;
using Badger.Sql.Abstractions;
using Badger.Sql.Abstractions.Errors;
using Badger.Sql.Abstractions.Extensions;
using Dapper;
using Idn.Contracts.V1;
using OneOf.Types;
using Tag.Contracts.V1;
using TagContracts;


namespace Tag.DataAccess;

public sealed class TagRepository : ITagRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly SqlQueries _queries;
    
    public TagRepository(IDbConnectionFactory connectionFactory, SqlQueries queries)
    {
        _connectionFactory = connectionFactory;
        _queries = queries;
    }
    
    public Task<OneOf<IReadOnlyList<TagValue>, UnreachableError>> ListAsync(UserId userId) =>
        _connectionFactory.TryAsync(async connection =>
        {
            var sqlQuery = _queries.GetCollectionsQuery(userId);

            var entities = await connection.QueryAsync<CollectionEntity>(sqlQuery);

            return entities.Select(entity => new TagValue(entity.Name)).ToReadonlyList();
        


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

}