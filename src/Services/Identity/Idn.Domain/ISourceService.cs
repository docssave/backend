namespace Idn.Domain;

internal interface ISourceService
{
    Task<string?> ExtractIdAsync(string token);
}