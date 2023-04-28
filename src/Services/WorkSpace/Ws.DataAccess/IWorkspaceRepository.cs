using Idn.Contracts;
using SqlServer.Abstraction;

namespace Ws.DataAccess;

public interface IWorkspaceRepository
{
    Task<RepositoryResult<Workspace?>> GetWorkspaceAsync(UserId userId);

    Task<RepositoryResult<Workspace>> CreateWorkspaceAsync(UserId userId, string name);
}