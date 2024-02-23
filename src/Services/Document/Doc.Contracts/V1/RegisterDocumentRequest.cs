using Col.Contracts.V1;

namespace Doc.Contracts.V1;

public sealed record RegisterDocumentRequest(CollectionId CollectionId, Document Document);