using Badger.Sql.Abstractions;
using Badger.SqlKata.Extensions;
using Col.Contracts.V1;
using Doc.Contracts.V1;
using SqlKata;

namespace Doc.Domain.V1.DataAccess;

internal sealed class SqlQueries(IQueryCompiler compiler)
{
    public string GetDocumentsQuery(CollectionId collectionId)
    {
        var query = new Query("Documents")
            .Select("Id", "Name", "Icon", "Version", "RegisteredAtTimespan")
            .Where("Documents.CollectionId", collectionId.Value)
            .OrderBy("RegisteredAtTimespan");

        return compiler.Compile(query);
    }

    public string GetDocumentVersionQuery(DocumentId documentId)
    {
        var query = new Query("Documents")
            .Select("Version")
            .Where("Documents.Id", documentId.Value);

        return compiler.Compile(query);
    }

    public string GetRegisterDocumentQuery(Document document, CollectionId collectionId)
    {
        var query = new Query("Documents")
            .AsUpsert(new KeyValuePair<string, object>[]
            {
                new("Id", document.DocumentId.Value),
                new("CollectionId", collectionId.Value),
                new("Name", document.Name),
                new("Icon", document.Icon),
                new("Version", document.Icon),
                new("RegisteredAtTimespan", document.RegisteredAt.ToUnixTimeMilliseconds())
            }, new KeyValuePair<string, object>[]
            {
                new("CollectionId", collectionId.Value),
                new("Name", document.Name),
                new("Icon", document.Icon),
                new("Version", document.Icon)
            });

        return compiler.Compile(query);
    }
}