using Doc.Contracts.V1;
using MediatR;
using OneOf;
using OneOf.Types;
using File = Doc.Contracts.V1.File;

namespace Doc.Contracts.V1;

public sealed record UploadFilesRequest(DocumentId DocumentId, File[] Files) : IRequest<OneOf<Success, Error<string>>>;