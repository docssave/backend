using Col.Contracts;
using Idn.Contracts;
using SqlServer.Abstraction;

namespace Col.DataAccess;

public sealed class CollectionRepository : ICollectionRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public CollectionRepository(IDbConnectionFactory connectionFactory) => 
        _connectionFactory = connectionFactory;
    
    public Task<IReadOnlyList<Collection>> ListCollectionsAsync(UserId userId)
    {
        throw new NotImplementedException();
    }

    public Task<Collection> RegisterCollectionAsync(CollectionId id, string name, string icon, EncryptSide encryptSide, int version)
    {
        throw new NotImplementedException();
    }
}
