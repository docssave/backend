using MediatR;

namespace Doc.Contracts.V1.Events;

public sealed record DocumentDeletedEvent(DocumentId DocumentId) : INotification;