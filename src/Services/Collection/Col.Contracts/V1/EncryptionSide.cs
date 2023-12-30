using System.Text.Json.Serialization;

namespace Col.Contracts.V1;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum EncryptionSide
{
    Client,
    Server
}