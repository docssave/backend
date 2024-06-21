using Badger.OneOf.Types;
using Col.Contracts.V1;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Doc.Contracts.V1;

public sealed record ListFilesRequest(CollectionId CollectionId, DocumentId DocumentId) : IRequest<OneOf<IReadOnlyList<File>, Unknown, Forbidden, Unreachable>>;