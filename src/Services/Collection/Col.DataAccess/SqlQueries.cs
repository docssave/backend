using Col.Contracts;
using Idn.Contracts;
using SqlKata;

namespace Col.DataAccess;

internal static class SqlQueries
{
    //TODO: SQL Server doesn't support ON DUPLICATE KYE
    public static Query RegisterCollectionQuery(CollectionId id, string name, string icon, EncryptSide encryptSide, int version) =>
        new Query("Collections")
            .AsInsert(new
            {
                Id = id.Value,
                Name = name,
                Icon = icon,
                EncryptSide = encryptSide,
                Version = version
            });

    public static Query RegisterUserCollectionQuery(UserId userId, CollectionId collectionId) =>
        new Query("UserCollections")
            .AsInsert(new
            {
                UserId = userId,
                CollectionId = collectionId
            });

    public static Query GetCollectionsQuery(UserId userId) =>
        new Query("Collections")
            .Select("Id", "Name", "Icon", "EncryptSide", "Version")
            .Join("UserCollections", "UserCollections.CollectionId", "Collections.Id")
            .Where("UserCollections.UserId", userId);

}