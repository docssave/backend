using Ws.Contracts.V1;

namespace Ws.Domain.V1.DataAccess;

public sealed class Workspace(WorkspaceId id, string name, DateTimeOffset addedAt)
{
    public WorkspaceId Id { get; } = id;

    public string Name { get; } = name;

    public DateTimeOffset AddedAt { get; } = addedAt;
}