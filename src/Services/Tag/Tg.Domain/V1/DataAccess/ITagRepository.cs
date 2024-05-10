using Badger.Sql.Abstractions.Errors;
using Idn.Contracts.V1;
using OneOf;
using OneOf.Types;
using Tg.Contracts.V1;

namespace Tg.Domain.V1.DataAccess;

internal interface ITagRepository
{
    Task<OneOf<Success, UnreachableError>> RegisterAsync(UserId userId, string value);

    Task<OneOf<IReadOnlyCollection<Tag>, UnreachableError>> GetAsync(UserId userId);
}