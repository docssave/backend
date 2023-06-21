using Idn.Contracts;
using Col.Contracts;

namespace Col.DataAccess;

public interface ICollectionRepository
{
    Task<IReadOnlyList<Collection>> ListCollectionsAsync(UserId userId);

    Task<Collection> RegisterCollectionAsync(CollectionId id, string name, string icon, EncryptSide encryptSide, int version);
}