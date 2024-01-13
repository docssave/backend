using Badger.Sql.Abstractions;
using Idn.Contracts.V1;
using SqlKata;

namespace Idn.Domain.V1.DataAccess;

internal sealed class SqlQueries(IQueryCompiler compiler)
{
    public string GetUserQuery(string sourceUserId)
    {
        var query = new Query("Users")
            .Select("Id", "Name", "EncryptedEmail", "Source", "SourceUserId", "RegisteredAtTimespan")
            .Where("SourceUserId", sourceUserId).Limit(1);

        return compiler.Compile(query);
    }

    public string CreateUserQuery(string sourceUserId, string name, string encryptedEmail, AuthorizationSource source, DateTimeOffset registeredAt)
    {
        var query = new Query("Users")
            .AsInsert(new
            {
                Name = name,
                EncryptedEmail = encryptedEmail,
                Source = source.ToString(),
                SourceUserId = sourceUserId,
                RegisteredAtTimespan = registeredAt.ToUnixTimeMilliseconds()
            }, returnId: true);

        return compiler.Compile(query);
    }
}