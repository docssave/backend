using Badger.Sql.Abstractions;
using Col.Contracts;
using Idn.Contracts;
using SqlKata;

namespace Col.Domain.DataAccess;

public sealed class SqlQueries
{
    private readonly IQueryCompiler _compiler;

    public SqlQueries(IQueryCompiler compiler) =>
        _compiler = compiler;

    //TODO: SQL Server doesn't support ON DUPLICATE KYE
    public string RegisterCollectionQuery(
        CollectionId id,
        string name,
        string icon,
        EncryptionSide encryptionSide,
        int version,
        DateTimeOffset addedAt)
    {
        var query = new Query("Collections")
            .AsInsert(new
            {
                Id = id.Value,
                Name = name,
                Icon = icon,
                EncryptSide = encryptionSide.ToString(),
                Version = version,
                AddedAtTimespan = addedAt.ToUnixTimeMilliseconds()
            });

        return _compiler.Compile(query);
    }

    public string RegisterUserCollectionQuery(UserId userId, CollectionId collectionId)
    {
        var query = new Query("UserCollections")
            .AsInsert(new
            {
                UserId = userId,
                CollectionId = collectionId
            });

        return _compiler.Compile(query);
    }

    public string GetCollectionsQuery(UserId userId)
    {
        var query = new Query("Collections")
            .Select("Id", "Name", "Icon", "EncryptSide", "Version", "AddedAtTimespan")
            .Join("UserCollections", "UserCollections.CollectionId", "Collections.Id")
            .Where("UserCollections.UserId", userId)
            .OrderBy("AddedAtTimespan");

        return _compiler.Compile(query);
    }
}