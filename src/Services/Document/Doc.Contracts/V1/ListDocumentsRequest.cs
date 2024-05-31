using Col.Contracts.V1;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Doc.Contracts.V1;

public sealed record ListDocumentsRequest(CollectionId CollectionId) : IRequest<OneOf<IReadOnlyList<Document>, Error<string>>>;