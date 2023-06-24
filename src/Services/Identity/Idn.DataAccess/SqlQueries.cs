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
        var query = new Query("Users").Where("SourceUserId", sourceUserId);

        return _compiler.Compile(query);
    }

    public string CreateUserQuery(CreateUser createUser, DateTimeOffset registeredAt)
    {
        var query = new Query("Users")
            .AsInsert(new
            {
                Name = createUser.Name,
                EncryptedEmail = createUser.EncryptedEmail,
                Source = createUser.Source.ToString(),
                SourceUserId = createUser.SourceUserId,
                RegisteredAt = registeredAt.ToUnixTimeMilliseconds()
            }, returnId: true);

        return _compiler.Compile(query);
    }
}