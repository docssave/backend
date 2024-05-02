﻿using Badger.OneOf.Types;
using Col.Contracts.V1;
using Doc.Contracts.V1;
using OneOf;
using OneOf.Types;
using File = Doc.Contracts.V1.File;

namespace Doc.Domain.V1.DataAccess;

internal interface IDocumentRepository
{
    Task<OneOf<IReadOnlyList<Document>, Unreachable<string>>> ListDocumentsAsync(Guid collectionId);

    Task<OneOf<Success, Unreachable<string>>> RegisterDocumentAsync(
        CollectionId collectionId,
        DocumentId documentId,
        string name,
        string icon,
        long? expectedVersion,
        File[] files);
}