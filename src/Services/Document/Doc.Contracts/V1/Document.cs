namespace Doc.Contracts.V1;

public sealed class Document(Guid documentId, string name, string icon, long version, DateTimeOffset registeredAt)
{
    public Guid DocumentId { get; } = documentId;

    public string Name { get; } = name;

    public string Icon { get; } = icon;

    public long Version { get; } = version;

    public DateTimeOffset RegisteredAt { get; } = registeredAt;
}