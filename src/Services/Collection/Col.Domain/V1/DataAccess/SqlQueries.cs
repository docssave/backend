using Badger.Sql.Abstractions;
using Badger.SqlKata.Extensions;
using Col.Contracts.V1;
using Idn.Contracts.V1;
using SqlKata;

namespace Col.Domain.V1.DataAccess;

public sealed class SqlQueries(IQueryCompiler compiler)
{
    public string RegisterCollectionQuery(
        CollectionId id,
        string name,
        string icon,
        EncryptionSide encryptionSide,
        int version,
        DateTimeOffset addedAt)
    {
        var query = new Query("Collections")
            .AsUpsert(new Dictionary<string, object>
            {
                { "Id", id.Value },
                { "Name", name},
                { "Icon", icon },
                { "EncryptSide", encryptionSide.ToString()},
                { "Version", version },
                { "AddedAtTimespan", addedAt.ToUnixTimeMilliseconds() }
            }, new Dictionary<string, object>
            {
                { "Name", name},
                { "Icon", icon },
                { "EncryptSide", encryptionSide.ToString()},
                { "Version", version },
                { "AddedAtTimespan", addedAt.ToUnixTimeMilliseconds() }
            });

        return compiler.Compile(query);
    }

    public string RegisterUserCollectionQuery(UserId userId, CollectionId collectionId)
    {
        var query = new Query("UserCollections")
            .AsInsert(new
            {
                UserId = userId,
                CollectionId = collectionId
            });

        return compiler.Compile(query);
    }

    public string GetCollectionsQuery(UserId userId)
    {
        var query = new Query("Collections")
            .Select("Id", "Name", "Icon", "EncryptSide", "Version", "AddedAtTimespan")
            .Join("UserCollections", "UserCollections.CollectionId", "Collections.Id")
            .Where("UserCollections.UserId", userId)
            .OrderBy("AddedAtTimespan");

        return compiler.Compile(query);
    }

    public string GetCollectionVersionQuery(CollectionId collectionId)
    {
        var query = new Query("Collections")
            .Select("Version")
            .Where("Collections", collectionId.Value);

        return compiler.Compile(query);
    }

    public string CheckCollectionExistingQuery(CollectionId collectionId)
    {
        var subQuery = new Query("Collections")
            .Select("1")
            .Where("Collections.Id", collectionId.Value);

        var compiledSubQuery = compiler.Compile(subQuery);

        var query = new Query("Collections")
            .SelectRaw($"EXISTS ({compiledSubQuery})");

        return compiler.Compile(query);
    }

    public string GetCollectionAccessQuery(UserId userId, CollectionId collectionId)
    {
        var subQuery = new Query("UserCollections")
            .Select("1")
            .Where("UserCollections.CollectionId", collectionId.Value)
            .Where("UserCollections.UserId", userId.Value);

        var compiledSubQuery = compiler.Compile(subQuery);

        var query = new Query("UserCollections")
            .SelectRaw($"EXISTS ({compiledSubQuery})");

        return compiler.Compile(query);
    }

    public string GetDeleteCollectionsQuery(UserId userId, CollectionId[] collectionIds)
    {
        var deletedCollectionsTableName = $"DeletedCollections_{Guid.NewGuid():N}";

        var createDeletedCollectionsTable = $"CREATE TEMPORARY TABLE IF NOT EXISTS {deletedCollectionsTableName} ";

        var userCollectionsQuery = new Query("UserCollections")
            .Where("UserId", userId)
            .WhereIn("CollectionId", collectionIds)
            .Select("CollectionId");

        var deleteCollectionsQuery = new Query("Collections")
            .WhereIn("CollectionId", userCollectionsQuery)
            .AsDelete();

        var deleteUserCollectionsQuery = new Query("UserCollections")
            .Where("UserId", userId)
            .WhereIn("CollectionId", userCollectionsQuery)
            .AsDelete();

        var selectCollectionIdsQuery = new Query(deletedCollectionsTableName)
            .Select("CollectionId");

        return $"{createDeletedCollectionsTable} {compiler.Compile(userCollectionsQuery)};" +
               compiler.Compile(deleteCollectionsQuery) + ";" +
               compiler.Compile(deleteUserCollectionsQuery) + ";" +
               compiler.Compile(selectCollectionIdsQuery);
    }
}