using MediatR;
using OneOf;
using OneOf.Types;

namespace Doc.Contracts.V1;

public sealed record ListFilesRequest(DocumentId DocumentId) : IRequest<OneOf<IReadOnlyList<File>, Error<string>>>;