using Badger.Sql.Abstractions;
using Idn.Contracts.V1;
using SqlKata;
using Tag.Contracts.V1;

namespace Tag.DataAccess;

internal sealed class SqlQueries
{
    private readonly IQueryCompiler _compiler;
    public SqlQueries(IQueryCompiler compiler)
    {
        _compiler = compiler;
    }

    public string GetTagsQuery(UserId userId)
    {
        var query = new Query("Tags")
            .Select("Value", "UserId")
            .Where("UserId", userId.Value);

        return _compiler.Compile(query);
    }

    public string GetRegisterTagQuery(TagValue tagValue, UserId userId)
    {
        var query = new Query("Tags")
            .AsInsert(new { Value = tagValue.Value, UserId = userId.Value });

        return _compiler.Compile(query);
    }
}