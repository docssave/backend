using MediatR;

namespace TagContracts;

public sealed record GetTagRequest() : IRequest<TagResponse>;