using MediatR;

namespace Col.Contracts.V1.Events;

public sealed record CollectionDeletedEvent(CollectionId CollectionId) : INotification;