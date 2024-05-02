using MediatR;
using OneOf;
using OneOf.Types;

namespace Doc.Contracts.V1;

public sealed record UploadFilesRequest(DocumentId DocumentId, File[] Files) : IRequest<OneOf<Success, Error<string>>>;