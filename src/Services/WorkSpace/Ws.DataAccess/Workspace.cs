using Ws.Contracts;

namespace Ws.DataAccess;

public sealed class Workspace
{
    public Workspace(WorkspaceId id, string name, DateTimeOffset registeredAt)
    {
        Id = id;
        Name = name;
        RegisteredAt = registeredAt;
    }

    public WorkspaceId Id { get; }
    
    public string Name { get; }

    public DateTimeOffset RegisteredAt { get; }
}