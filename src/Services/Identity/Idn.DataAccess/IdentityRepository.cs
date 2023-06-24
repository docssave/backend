using Dapper;
using Idn.Contracts;
using OneOf;
using OneOf.Types;
using Sql.Abstractions;
using Sql.Abstractions.Errors;
using Sql.Abstractions.Extensions;

namespace Idn.DataAccess;

public sealed class IdentityRepository : IIdentityRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly SqlQueries _sqlQueries;

    public IdentityRepository(IDbConnectionFactory connectionFactory, SqlQueries sqlQueries)
    {
        _connectionFactory = connectionFactory;
        _sqlQueries = sqlQueries;
    }

    public Task<OneOf<User, NotFound, UnreachableError>> GetUserAsync(string sourceUserId) =>
        _connectionFactory.TryAsync(async connection =>
        {
            var sqlQuery = _sqlQueries.GetUserQuery(sourceUserId);

            var entity = await connection.QuerySingleOrDefaultAsync<UserEntity>(sqlQuery);

            var user = entity == null
                ? null
                : new User(
                    new UserId(entity.Id),
                    entity.Name,
                    entity.EncryptedEmail,
                    Enum.Parse<AuthorizationSource>(entity.Source),
                    DateTimeOffset.FromUnixTimeMilliseconds(entity.RegisteredAt));

            return RepositoryResult<User?>.Success(user);
        }, RepositoryResult<User>.Failed);

    public Task<OneOf<User, UnreachableError>> CreateUserAsync(CreateUser createUser) =>
        _connectionFactory.TryAsync(async connection =>
        {
            var now = DateTimeOffset.UtcNow;
            
            var sqlQuery = _sqlQueries.CreateUserQuery(createUser, now);

            var userId = await connection.QuerySingleAsync<long>(sqlQuery);
            var user = new User(new UserId(userId), createUser.Name, createUser.EncryptedEmail, createUser.Source, now);

            return RepositoryResult<User>.Success(user);
        }, RepositoryResult<User>.Failed);

    private sealed record UserEntity(long Id, string Name, string EncryptedEmail, string Source, string SourceUserId, long RegisteredAt);
}