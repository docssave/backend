using MediatR;

namespace TagContracts;

public sealed record TagRequest(string Name) : IRequest<TagResponse>;