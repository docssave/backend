using System.Text.Json.Serialization;

namespace Idn.Contracts.V1;

[JsonConverter(typeof(JsonStringEnumMemberConverter))]
public enum AuthorizationSource : byte
{
    Google,
    Apple,
    Microsoft,
    Facebook
} 