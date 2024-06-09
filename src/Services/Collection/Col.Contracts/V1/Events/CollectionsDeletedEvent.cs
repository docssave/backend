using MediatR;

namespace Col.Contracts.V1.Events;

public sealed record CollectionsDeletedEvent(CollectionId[] CollectionIds) : INotification;