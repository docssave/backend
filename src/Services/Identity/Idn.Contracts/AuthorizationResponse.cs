using Common;

namespace Idn.Contracts;

public record AuthorizationResponse(string Token) : ResponseBase<string>;