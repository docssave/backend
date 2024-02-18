﻿using Badger.Collections.Extensions;
using Badger.Sql.Abstractions;
using Badger.Sql.Abstractions.Errors;
using Badger.Sql.Abstractions.Extensions;
using Dapper;
using Idn.Contracts.V1;
using OneOf;
using OneOf.Types;
using Tag.Contracts.V1;
using TagContracts;


namespace Tag.DataAccess;

internal sealed class TagRepository : ITagRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly SqlQueries _queries;
    
    public TagRepository(IDbConnectionFactory connectionFactory, SqlQueries queries)
    {
        _connectionFactory = connectionFactory;
        _queries = queries;
    }

    public Task<OneOf<Success, UnreachableError>> RegisterAsync(UserId userId, TagValue tagValue, DateTimeOffset addedAt) => 
        _connectionFactory.TryAsync(async (connection, transaction) =>
        {
            var createCollectionQuery = _queries.GetRegisterTagQuery(tagValue, userId);

            await connection.ExecuteAsync(createCollectionQuery, transaction: transaction);

            return new Success();
        }, ToUnreachableError);

    private static UnreachableError ToUnreachableError(Exception exception) => new(exception.Message);

    private sealed record TagEntity(string Value, Guid UserId);

    public Task<OneOf<IReadOnlyList<TagValue>, UnreachableError>> ListAsync(UserId userId) =>
        _connectionFactory.TryAsync(async connection =>
        {
            var sqlQuery = _queries.GetTagsQuery(userId);

            var entities = await connection.QueryAsync<TagEntity>(sqlQuery);

            return entities.Select(entity => new TagValue(entity.Value)).ToReadonlyList();
        }, ToUnreachableError);
}

