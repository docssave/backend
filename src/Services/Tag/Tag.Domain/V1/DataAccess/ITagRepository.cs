using Badger.Sql.Abstractions.Errors;
using Idn.Contracts.V1;
using OneOf;
using OneOf.Types;
using Tag.Contracts.V1;

namespace Tag.DataAccess;

internal interface ITagRepository
{
    Task<OneOf<IReadOnlyList<TagValue>, UnreachableError>> ListAsync(UserId userId);

    Task<OneOf<Success, UnreachableError>> RegisterAsync(
        UserId userId,
        TagValue tagValue,
        DateTimeOffset addedAt);
}