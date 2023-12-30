using MediatR;
using TagContracts;

namespace Tag.Domain.Handlers;

internal sealed class CreateTagHandler : IRequestHandler<CreateTagRequest, TagResponse>
{
    public Task<TagResponse> Handle(CreateTagRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}