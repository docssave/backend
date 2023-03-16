using MediatR;

namespace Idn.Contracts;

public sealed record AuthorizationRequest(string Token, Source Source) : IRequest<AuthorizationResponse>;