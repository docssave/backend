using Idn.Contracts;
using Sql.Abstractions;
using SqlKata;
using Ws.Contracts;

namespace Ws.DataAccess;

public sealed class SqlQueries
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
                RegisteredAtTimespan = registeredAt.ToUnixTimeMilliseconds()
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
            .Select("Id", "Name", "RegisteredAtTimespan")
            .Join("UserWorkspaces", "UserWorkspaces.WorkspaceId", "Workspaces.Id")
            .Where("UserWorkspaces.UserId", userId)
            .OrderByDesc("RegisteredAtTimespan");

        return _compiler.Compile(query);
    }
}