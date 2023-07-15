using Badger.Sql.Abstractions;
using Idn.Contracts.V1;
using SqlKata;
using Ws.Contracts;
using Ws.Contracts.V1;

namespace Ws.Domain.V1.DataAccess;

internal sealed class SqlQueries
{
    private readonly IQueryCompiler _compiler;

    public SqlQueries(IQueryCompiler compiler)
    {
        _compiler = compiler;
    }
    
    public string RegisterWorkspaceQuery(WorkspaceId id, string name, DateTimeOffset registeredAt)
    {
        var query = new Query("Workspaces")
            .AsInsert(new
            {
                Id = id.Value,
                Name = name,
                AddedAtTimespan = registeredAt.ToUnixTimeMilliseconds()
            });

        return _compiler.Compile(query);
    }

    public string RegisterUserWorkspaceQuery(UserId userId, WorkspaceId workspaceId)
    {
        var query = new Query("UserWorkspaces")
            .AsInsert(new
            {
                UserId = userId.Value,
                WorkspaceId = workspaceId.Value
            });

        return _compiler.Compile(query);
    }

    public string GetWorkspaceQuery(UserId userId)
    {
        var query = new Query("Workspaces")
            .Select("Id", "Name", "AddedAtTimespan")
            .Join("UserWorkspaces", "UserWorkspaces.WorkspaceId", "Workspaces.Id")
            .Where("UserWorkspaces.UserId", userId)
            .OrderByDesc("AddedAtTimespan");

        return _compiler.Compile(query);
    }
}