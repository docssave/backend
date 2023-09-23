﻿namespace Idn.Contracts.Options;

public sealed class TokenOptions
{
    public const string SectionName = "TokenOptions";
    
    public string ClientSecret { get; set; }

    public string Audience { get; set; }

    public string Issuer { get; set; }
}