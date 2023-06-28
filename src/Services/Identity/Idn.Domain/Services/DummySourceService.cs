using OneOf;
using OneOf.Types;

namespace Idn.Domain.Services;

internal sealed class DummySourceService : ISourceService
{
    public Task<OneOf<SourceUserInfo, Error<string>>> ExtractUserInfoAsync(string token)
    {
        return Task.FromResult(OneOf<SourceUserInfo, Error<string>>.FromT0(new SourceUserInfo(token, "No name", "test@docssave.com")));
    }
}