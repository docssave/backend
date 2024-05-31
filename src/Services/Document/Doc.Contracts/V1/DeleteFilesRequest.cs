using Badger.OneOf.Types;
using Col.Contracts.V1;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Doc.Contracts.V1;

public sealed record DeleteFilesRequest(CollectionId CollectionId, DocumentId DocumentId, FileId[] FileIds) : IRequest<OneOf<Success, Unknown, Forbidden, Unreachable>>;