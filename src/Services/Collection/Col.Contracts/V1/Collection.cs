namespace Col.Contracts.V1;

public sealed class Collection
{
    public Collection(CollectionId id, string name, string icon, EncryptionSide encryptionSide, int version, DateTimeOffset addedAt)
    {
        Id = id;
        Name = name;
        Icon = icon;
        EncryptionSide = encryptionSide;
        Version = version;
        AddedAt = addedAt;
    }

    public CollectionId Id { get; }
    
    public string Name { get; }
    
    public string Icon { get; }
    
    public EncryptionSide EncryptionSide { get; }
    
    public int Version { get; }
    
    public DateTimeOffset AddedAt { get; }
}