using Badger.Sql.Abstractions;
using Badger.SqlKata.Extensions;
using Idn.Contracts.V1;
using SqlKata;
using Tg.Contracts.V1;

namespace Tg.Domain.V1.DataAccess;

internal sealed class SqlQueries(IQueryCompiler compiler)
{
    public string GetTagsQuery(UserId userId)
    {
        var query = new Query("Tags")
            .Select("Value")
            .Where("UserId", userId.Value);

        return compiler.Compile(query);
    }

    public string GetRegisterTagQuery(Tag tag, UserId userId)
    {
        var query = new Query("Tags")
            .AsUpsert(new Dictionary<string, object>
            {
                {"Value", tag.Value},
                {"UserId", userId.Value}
            }, new Dictionary<string, object>
            {
                {"Value", tag.Value},
            });

        return compiler.Compile(query);
    }
}