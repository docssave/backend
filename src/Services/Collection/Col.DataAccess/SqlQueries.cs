using Col.Contracts;
using SqlKata;

namespace Col.DataAccess;

internal static class SqlQueries
{
    public static Query RegisterCollectionQuery(CollectionId id, string name, string icon, EncryptSide encryptSide, int version) =>
        new Query("Collections")
            .AsInsert(new
            {
                Id = id.Value,
                Name = name,
                Icon = icon,
                EncryptSide = encryptSide,
                Version = version
            }).;
}