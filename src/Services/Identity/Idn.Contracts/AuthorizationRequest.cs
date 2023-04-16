using MediatR;

namespace Idn.Contracts;

public sealed record AuthorizationRequest(string Token, AuthorizationSource? Source) : IRequest<AuthorizationResponse>;