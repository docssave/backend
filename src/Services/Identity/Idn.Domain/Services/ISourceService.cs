namespace Idn.Domain;

internal interface ISourceService
{
    Task<SourceUserInfo> ExtractUserInfoAsync(string token);
}