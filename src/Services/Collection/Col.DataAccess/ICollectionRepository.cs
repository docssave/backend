using Idn.Contracts;

namespace Col.DataAccess;

public interface ICollectionRepository
{
    Task<IReadOnlyList<Collection>> ListCollectionsAsync(UserId notificationId);
}