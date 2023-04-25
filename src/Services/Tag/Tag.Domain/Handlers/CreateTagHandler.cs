using MediatR;
using TagContracts;

namespace Tag.Domain.Handlers;

internal sealed class CreateTagHandler : IRequestHandler<TagRequest, TagResponse>
{
    public Task<TagResponse> Handle(TagRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}