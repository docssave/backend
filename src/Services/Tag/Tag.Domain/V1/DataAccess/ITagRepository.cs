using Badger.Sql.Abstractions.Errors;
using OneOf;
using OneOf.Types;

namespace Tag.DataAccess;

public interface ITagRepository
{
    Task<OneOf<IReadOnlyList<Tag>, UnreachableError>> ListAsync(UserId userId);

    Task<OneOf<Success, UnreachableError>> RegisterAsync(
        UserId userId,
        TagId id,
        string name,
        DateTimeOffset addedAt);
}