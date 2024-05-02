using Badger.OneOf.Types;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Tg.Contracts.V1;

public sealed record RegisterTagRequest(string Value) : IRequest<OneOf<Success, Unknown, Unreachable>>;