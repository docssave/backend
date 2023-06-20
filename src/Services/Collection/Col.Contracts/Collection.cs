namespace Col.Contracts;

public sealed class Collection
{
    public CollectionId Id { get; }
    
    public string Name { get; }
    
    public string Icon { get; }
    
    public EncryptSide EncryptSide { get; }
    
    public int Version { get; }
}