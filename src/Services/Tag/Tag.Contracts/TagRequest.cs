using MediatR;

namespace TagContracts;

public sealed record TagRequest(Tag tag, Source Source) : IRequest<TagResponse>;