using Common;

namespace Idn.Contracts;

public record AuthorizationResponse(string? Token, Error? Error) : ResponseBase(Error);