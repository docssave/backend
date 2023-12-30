using MediatR;

namespace TagContracts;

public sealed record CreateTagRequest(string Name) : IRequest<TagResponse>;