using Dapper;
using Idn.Contracts;
using SqlServer.Abstraction;
using SqlServer.Abstraction.Extensions;

namespace Idn.DataAccess;

public sealed class IdentityRepository : RepositoryBase, IIdentityRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public IdentityRepository(IDbConnectionFactory connectionFactory) =>
        _connectionFactory = connectionFactory;

    public Task<RepositoryResult<User>> GetUserAsync(string sourceUserId) =>
        _connectionFactory.TryAsync(async connection =>
        {
            var sqlQuery = QueryCompiler.ToSqlQueryString(SqlQueries.GetUserQuery(sourceUserId));

            var entity = await connection.QuerySingleOrDefaultAsync<UserEntity>(sqlQuery);

            var user = entity == null
                ? null
                : new User(new UserId(entity.Id), entity.Name, entity.EncryptedEmail, Enum.Parse<AuthorizationSource>(entity.Source), DateTimeOffset.FromUnixTimeMilliseconds(entity.RegisteredAt));

            return RepositoryResult<User?>.Success(user);
        }, RepositoryResult<User>.Failed);

    public Task<RepositoryResult<User>> CreateUserAsync(CreateUser createUser) =>
        _connectionFactory.TryAsync(async connection =>
        {
            var now = DateTimeOffset.UtcNow;
            
            var sqlQuery = QueryCompiler.ToSqlQueryString(SqlQueries.CreateUserQuery(createUser, now));

            var userId = await connection.QuerySingleAsync<long>(sqlQuery);
            var user = new User(new UserId(userId), createUser.Name, createUser.EncryptedEmail, createUser.Source, now);

            return RepositoryResult<User>.Success(user);
        }, RepositoryResult<User>.Failed);

    private sealed record UserEntity(long Id, string Name, string EncryptedEmail, string Source, string SourceUserId, long RegisteredAt);
}