using Badger.Collections.Extensions;
using Badger.Sql.Abstractions;
using Badger.Sql.Abstractions.Errors;
using Badger.Sql.Abstractions.Extensions;
using Dapper;
using Idn.Contracts.V1;
using OneOf;
using OneOf.Types;
using Ws.Contracts;
using Ws.Contracts.V1;

namespace Ws.Domain.V1.DataAccess;

internal sealed class WorkspaceRepository : IWorkspaceRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly SqlQueries _queries;

    public WorkspaceRepository(IDbConnectionFactory connectionFactory, SqlQueries queries)
    {
        _connectionFactory = connectionFactory;
        _queries = queries;
    }

    public Task<OneOf<IReadOnlyList<Workspace>, UnreachableError>> ListAsync(UserId userId) =>
        _connectionFactory.TryAsync(async connection =>
        {
            var sqlQuery = _queries.GetWorkspaceQuery(userId);

            var entities = await connection.QueryAsync<WorkspaceEntity>(sqlQuery);

            return entities
                .Select(entity => new Workspace(
                    new WorkspaceId(entity.Id),
                    entity.Name,
                    DateTimeOffset.FromUnixTimeMilliseconds(entity.AddedAtTimespan)))
                .ToReadonlyList();
        }, ToUnreachableError);

    public Task<OneOf<Success, UnreachableError>> RegisterAsync(WorkspaceId id, string name,  UserId userId, DateTimeOffset registeredAt) =>
        _connectionFactory.TryAsync(async (connection, transaction) =>
        {
            var createWorkspaceQuery = _queries.RegisterWorkspaceQuery(id, name, registeredAt);

            await connection.ExecuteAsync(createWorkspaceQuery, transaction: transaction);

            var createUserWorkspaceQuery = _queries.RegisterUserWorkspaceQuery(userId, id);

            await connection.ExecuteAsync(createUserWorkspaceQuery, transaction: transaction);

            return new Success();
        }, ToUnreachableError);
    
    private static UnreachableError ToUnreachableError(Exception exception) => new(exception.Message);

    private sealed record WorkspaceEntity(Guid Id, string Name, long AddedAtTimespan);
}