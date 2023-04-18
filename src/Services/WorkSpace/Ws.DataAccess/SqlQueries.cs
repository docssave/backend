using Idn.Contracts;
using SqlKata;

namespace Ws.DataAccess;

internal static class SqlQueries
{
    public static Query CreateWorkspaceQuery(string name) =>
        new Query("Workspaces")
            .AsInsert(new
            {
                Name = name
            }, returnId: true);

    public static Query CreateUserWorkspaceQuery(UserId userId, long workspaceId) =>
        new Query("UserWorkspaces")
            .AsInsert(new
            {
                UserId = userId,
                WorkspaceId = workspaceId
            });

    public static Query GetWorkspaceQuery(UserId userId) =>
        new Query("Workspaces w")
            .Select("w.Id", "w.Name")
            .Join("UserWorkspaces uw", "uw.WorkspaceId", "w.Id")
            .Where("uw.UserId", userId);
}