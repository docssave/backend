using Doc.Contracts.V1;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Fl.Contracts.V1;

public sealed record ListFilesRequest(DocumentId DocumentId) : IRequest<OneOf<IReadOnlyList<File>, Error<string>>>;