namespace Idn.Domain;

internal sealed class DummySourceService : ISourceService
{
    public Task<SourceUserInfo> ExtractUserInfoAsync(string token)
    {
        return Task.FromResult(new SourceUserInfo(token, "No name", "test@docssave.com"));
    }
}