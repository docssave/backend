namespace Doc.Plugin.V1.Dtos;

public sealed record RegisterDocumentDto(string Name, string Icon, long? ExpectedVersion);