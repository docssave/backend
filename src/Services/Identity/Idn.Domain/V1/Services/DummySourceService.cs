using OneOf;
using OneOf.Types;

namespace Idn.Domain.V1.Services;

internal sealed class DummySourceService : ISourceService
{
    public Task<OneOf<SourceUserInfo, Error<string>>> ExtractUserInfoAsync(string token)
    {
        if (token.Length >= 500)
        {
            token = token[..499];
        }
        
        return Task.FromResult(OneOf<SourceUserInfo, Error<string>>.FromT0(new SourceUserInfo(token, "No name", "test@docssave.com")));
    }
}