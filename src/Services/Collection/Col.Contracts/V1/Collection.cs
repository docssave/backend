namespace Col.Contracts.V1;

public sealed class Collection(CollectionId id, string name, string icon, EncryptionSide encryptionSide, long version, DateTimeOffset addedAt)
{
    public CollectionId Id { get; } = id;

    public string Name { get; } = name;

    public string Icon { get; } = icon;

    public EncryptionSide EncryptionSide { get; } = encryptionSide;

    public long Version { get; } = version;

    public DateTimeOffset AddedAt { get; } = addedAt;
}