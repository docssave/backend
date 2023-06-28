using Idn.Contracts;
using Sql.Abstractions;
using SqlKata;

namespace Idn.DataAccess;

public sealed class SqlQueries
{
    private readonly IQueryCompiler _compiler;

    public SqlQueries(IQueryCompiler compiler)
    {
        _compiler = compiler;
    }
    
    public string GetUserQuery(string sourceUserId)
    {
        var query = new Query("Users").Where("SourceUserId", sourceUserId).Limit(1);

        return _compiler.Compile(query);
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
                RegisteredAt = registeredAt.ToUnixTimeMilliseconds()
            }, returnId: true);

        return _compiler.Compile(query);
    }
}