using Col.Contracts.V1;

namespace Col.Plugin.V1.Dtos;

public sealed record RegisterCollectionDto(string Name, string Icon, EncryptionSide EncryptionSide, int? Version);