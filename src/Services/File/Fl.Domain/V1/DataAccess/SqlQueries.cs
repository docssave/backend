﻿using Badger.Sql.Abstractions;
using Badger.SqlKata.Extensions;
using Doc.Contracts.V1;
using Fl.Contracts.V1;
using SqlKata;

namespace Fl.Domain.V1.DataAccess;

internal sealed class SqlQueries(IQueryCompiler compiler)
{
    public string RegisterFileQuery(FileId fileId, byte[] content)
    {
        var query = new Query("Files")
            .AsUpsert(new Dictionary<string, object>
            {
                {"Id", fileId.Value},
                {"Content", content}
            }, new Dictionary<string, object>
            {
                {"Content", content}
            });

        return compiler.Compile(query);
    }

    public string RegisterFileMetadataQuery(FileId fileId, DocumentId documentId, long size, string mimeType, DateTimeOffset registeredAt)
    {
        var query = new Query("FileMetadata")
            .AsUpsert(new Dictionary<string, object>
            {
                { "FileId", fileId.Value },
                { "DocumentId", documentId },
                { "Size", size },
                { "Type", mimeType },
                { "RegisteredAtTimestamp", registeredAt.ToUnixTimeMilliseconds() }
            }, new Dictionary<string, object>
            {
                { "Size", size },
                { "Type", mimeType },
                { "RegisteredAtTimestamp", registeredAt.ToUnixTimeMilliseconds() }
            });

        return compiler.Compile(query);
    }

    public string GetFilesQuery(DocumentId documentId)
    {
        var query = new Query("Files")
            .Select("Id", "Content", "Size", "Type", "RegisteredAtTimestamp")
            .Join("FileMetadata", "Files.Id", "FileMetadata.FileId")
            .Where("FileMetadata.DocumentId", documentId.Value)
            .OrderBy("RegisteredAtTimestamp");

        return compiler.Compile(query);
    }

    public string DeleteFiles(FileId[] fileIds)
    {
        var query = new Query("Files")
            .WhereIn("Files.Id", fileIds.Select(fileId => fileId.Value))
            .AsDelete();

        return compiler.Compile(query);
    }

    public string DeleteFilesMetadata(FileId[] fileIds)
    {
        var query = new Query("FileMetadata")
            .WhereIn("FileMetadata.FileId", fileIds.Select(fileId => fileId.Value))
            .AsDelete();

        return compiler.Compile(query);
    }

    public string GetFileIds(DocumentId documentId)
    {
        var query = new Query("FileMetadata")
            .Select("FileId")
            .Where("FileMetadata.DocumentId", documentId.Value);

        return compiler.Compile(query);
    }
}