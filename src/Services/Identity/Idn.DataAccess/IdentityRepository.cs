using Dapper;
using Idn.Contracts;
using SqlKata;
using SqlKata.Compilers;
using SqlServer;
using SqlServer.Abstraction;
using SqlServer.Abstraction.Extensions;

namespace Idn.DataAccess;

public sealed class IdentityRepository : IIdentityRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly SqlServerCompiler _queryCompiler;
        
    public IdentityRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
        _queryCompiler = new SqlServerCompiler();
    }

    public Task<RepositoryResult<User>> GetUserAsync(string sourceUserId) =>
        _connectionFactory.TryAsync(async connection =>
        {
            var query = new Query("Users").Where("SourceUserId", sourceUserId);
            var result = _queryCompiler.Compile(query);

            var entity = await connection.QuerySingleOrDefaultAsync<UserEntity>(result.Sql);

            var user = entity == null
                ? null
                : new User(new UserId(entity.Id), entity.Name, entity.EncryptedEmail, Enum.Parse<AuthorizationSource>(entity.Source));

            return new RepositoryResult<User?>(user, null);
        }, exception => new RepositoryResult<User>(null, exception));

    public Task<RepositoryResult<User>> CreateUserAsync(CreateUser user)
    {
        throw new NotImplementedException();
    }

    private sealed record UserEntity(long Id, string Name, string EncryptedEmail, string Source, string SourceUserId);
}