using MediatR;
using Tag.DataAccess.Tag;
using Tag.Extensions;

namespace Tag.Contracts;

public sealed record TagRequest(Tag tag, Source Source) : IRequest<TagResponse>;


