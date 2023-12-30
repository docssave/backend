using Badger.Sql.Abstractions;
using Badger.Sql.Abstractions.Errors;
using Badger.Sql.Abstractions.Extensions;
using Dapper;
using Idn.Contracts.V1;
using OneOf;
using OneOf.Types;

namespace Idn.Domain.V1.DataAccess;

internal sealed class IdentityRepository : IIdentityRepository
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

            if (entity == null)
            {
                return OneOf<User, NotFound>.FromT1(new NotFound());
            }

            return new User(
                    new UserId(entity.Id),
                    entity.Name,
                    entity.EncryptedEmail,
                    Enum.Parse<AuthorizationSource>(entity.Source),
                    DateTimeOffset.FromUnixTimeMilliseconds(entity.RegisteredAtTimespan));
        }, ToUnreachableError);

    public Task<OneOf<User, UnreachableError>> RegisterUserAsync(string sourceUserId, string name, string encryptedEmail, AuthorizationSource source, DateTimeOffset registeredAt) =>
        _connectionFactory.TryAsync(async connection =>
        {
            var sqlQuery = _sqlQueries.CreateUserQuery(sourceUserId, name, encryptedEmail, source, registeredAt);

            var userId = await connection.QuerySingleAsync<long>(sqlQuery);
            var user = new User(new UserId(userId), name, encryptedEmail, source, registeredAt);

            return user;
        }, ToUnreachableError);
    
    private static UnreachableError ToUnreachableError(Exception exception) => new(exception.Message);

    private sealed record UserEntity(long Id, string Name, string EncryptedEmail, string Source, string SourceUserId, long RegisteredAtTimespan);
}