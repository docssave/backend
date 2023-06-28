using OneOf;
using OneOf.Types;

namespace Idn.Domain.Services;

internal interface ISourceService
{
    Task<OneOf<SourceUserInfo, Error<string>>> ExtractUserInfoAsync(string token);
}