namespace Idn.Domain.V1.Options;

public sealed class TokenOptions
{
    public const string SectionName = "TokenOptions";

    public string ClientSecret { get; set; } = null!;

    public string Audience { get; set; } = null!;

    public string Issuer { get; set; } = null!;
}