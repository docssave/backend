using OneOf;
using OneOf.Types;

namespace Idn.Domain.V1.Services;

internal interface ISourceService
{
    Task<OneOf<SourceUserInfo, Error<string>>> ExtractUserInfoAsync(string token);
}