using Badger.Sql.Abstractions;
using Col.Contracts.V1;
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
}