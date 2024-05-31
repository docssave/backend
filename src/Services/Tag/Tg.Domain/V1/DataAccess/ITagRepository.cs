using Badger.OneOf.Types;
using Idn.Contracts.V1;
using OneOf;
using OneOf.Types;
using Tg.Contracts.V1;

namespace Tg.Domain.V1.DataAccess;

internal interface ITagRepository
{
    Task<OneOf<Success, Unreachable<string>>> RegisterAsync(UserId userId, string value);

    Task<OneOf<IReadOnlyCollection<Tag>, Unreachable<string>>> GetAsync(UserId userId);
}