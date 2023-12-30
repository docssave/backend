using MediatR;
using OneOf;
using OneOf.Types;

namespace Idn.Contracts.V1;

public sealed record AuthorizationRequest(string Token, AuthorizationSource? Source) : IRequest<OneOf<AuthorizationResponse, Error<string>>>;