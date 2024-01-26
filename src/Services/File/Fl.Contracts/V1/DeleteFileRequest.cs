using MediatR;
using OneOf;
using OneOf.Types;

namespace Fl.Contracts.V1;

public sealed record DeleteFileRequest(FileId FileId) : IRequest<OneOf<Success, Error<string>>>;