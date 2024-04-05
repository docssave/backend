using MediatR;
using OneOf;
using OneOf.Types;

namespace Doc.Contracts.V1;

public sealed record ListDocumentsRequest(Guid CollectionId) : IRequest<OneOf<IReadOnlyList<Document>, Error<string>>>;