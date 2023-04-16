using SqlKata;

namespace Idn.DataAccess;

internal static class SqlQueries
{
    public static Query GetUserQuery(string sourceUserId) => new Query("Users").Where("SourceUserId", sourceUserId);

    public static Query CreateUserQuery(CreateUser createUser, DateTimeOffset registeredAt) => new Query("Users")
        .AsInsert(new
        {
            Name = createUser.Name,
            EncryptedEmail = createUser.EncryptedEmail,
            Source = createUser.Source.ToString(),
            SourceUserId = createUser.SourceUserId,
            RegisteredAt = registeredAt.ToUnixTimeMilliseconds()
        }, returnId: true);
}