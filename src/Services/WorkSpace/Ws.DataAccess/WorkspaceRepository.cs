using Dapper;
using Idn.Contracts;
using SqlServer.Abstraction;
using SqlServer.Abstraction.Extensions;
using Ws.Contracts;

namespace Ws.DataAccess;

public sealed class WorkspaceRepository : RepositoryBase, IWorkspaceRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public WorkspaceRepository(IDbConnectionFactory connectionFactory) =>
        _connectionFactory = connectionFactory;

    public Task<RepositoryResult<Workspace?>> GetWorkspaceAsync(UserId userId) =>
        _connectionFactory.TryAsync(async connection =>
        {
            var sqlQuery = QueryCompiler.ToSqlQueryString(SqlQueries.GetWorkspaceQuery(userId));

            var entity = await connection.QuerySingleOrDefaultAsync<WorkspaceEntity>(sqlQuery);

            var workspace = entity == null
                ? null
                : new Workspace(new WorkspaceId(entity.Id), entity.Name);

            return RepositoryResult<Workspace?>.Success(workspace);
        }, RepositoryResult<Workspace?>.Failed);

    public Task<RepositoryResult<Workspace>> CreateWorkspaceAsync(UserId userId, string name) =>
        _connectionFactory.TryAsync(async (connection, transaction) =>
        {
            var createWorkspaceQuery = QueryCompiler.ToSqlQueryString(SqlQueries.CreateWorkspaceQuery(name));

            var workspaceId = await connection.QuerySingleOrDefaultAsync<long>(createWorkspaceQuery, transaction: transaction);

            var createUserWorkspaceQuery = QueryCompiler.ToSqlQueryString(SqlQueries.CreateUserWorkspaceQuery(userId, workspaceId));

            await connection.ExecuteAsync(createUserWorkspaceQuery, transaction: transaction);

            return RepositoryResult<Workspace>.Success(new Workspace(new WorkspaceId(workspaceId), name));
        }, RepositoryResult<Workspace>.Failed);

    private sealed record WorkspaceEntity(long Id, string Name);
}