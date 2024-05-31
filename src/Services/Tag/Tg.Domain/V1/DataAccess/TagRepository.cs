using Badger.Collections.Extensions;
using Badger.OneOf.Types;
using Badger.Sql.Abstractions;
using Badger.Sql.Abstractions.Extensions;
using Dapper;
using Idn.Contracts.V1;
using OneOf;
using OneOf.Types;
using Tg.Contracts.V1;

namespace Tg.Domain.V1.DataAccess;

internal sealed class TagRepository(IDbConnectionFactory connectionFactory, SqlQueries queries) : ITagRepository
{
    public Task<OneOf<Success, Unreachable<string>>> RegisterAsync(UserId userId, string value) =>
        connectionFactory.TryAsync(async (connection, transaction) =>
        {
            var createCollectionQuery = queries.GetRegisterTagQuery(value, userId);

            await connection.ExecuteAsync(createCollectionQuery, transaction: transaction);

            return new Success();
        }, ToUnreachableError);

    public Task<OneOf<IReadOnlyCollection<Tag>, Unreachable<string>>> GetAsync(UserId userId) =>
        connectionFactory.TryAsync(async connection =>
        {
            var query = queries.GetTagsQuery(userId);

            var entities = await connection.QueryAsync<TagEntity>(query);

            return entities.Select(entity => new Tag(entity.Value)).ToReadOnlyCollection();
        }, ToUnreachableError);

    private static Unreachable<string> ToUnreachableError(Exception exception) => new(exception.Message);

    private sealed record TagEntity(string Value);
}