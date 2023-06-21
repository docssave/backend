namespace Col.Contracts;

public sealed class Collection
{
    public Collection(CollectionId id, string name, string icon, EncryptSide encryptSide, int version)
    {
        Id = id;
        Name = name;
        Icon = icon;
        EncryptSide = encryptSide;
        Version = version;
    }

    public CollectionId Id { get; }
    
    public string Name { get; }
    
    public string Icon { get; }
    
    public EncryptSide EncryptSide { get; }
    
    public int Version { get; }
}