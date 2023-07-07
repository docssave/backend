using Ws.Contracts;

namespace Ws.Domain.DataAccess;

public sealed class Workspace
{
    public Workspace(WorkspaceId id, string name, DateTimeOffset addedAt)
    {
        Id = id;
        Name = name;
        AddedAt = addedAt;
    }

    public WorkspaceId Id { get; }
    
    public string Name { get; }

    public DateTimeOffset AddedAt { get; }
}