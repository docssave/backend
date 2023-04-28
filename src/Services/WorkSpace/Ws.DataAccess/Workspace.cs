using Ws.Contracts;

namespace Ws.DataAccess;

public sealed class Workspace
{
    public Workspace(WorkspaceId id, string name)
    {
        Id = id;
        Name = name;
    }

    public WorkspaceId Id { get; }
    
    public string Name { get; }
}