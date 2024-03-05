using Badger.Service.Error;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Col.Contracts.V1;

public sealed record CheckCollectionExistingRequest(CollectionId CollectionId) : IRequest<OneOf<Success, NotFoundServiceError, UnexpectedServiceError>>;