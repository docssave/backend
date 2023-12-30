using SqlKata;
using SqlKata.Compilers;
using Sql.Abstractions;

namespace SqlServer;

public sealed class SqlQueryCompiler : IQueryCompiler
{
    private readonly SqlServerCompiler _compiler;

    public SqlQueryCompiler(SqlServerCompiler compiler) =>
        _compiler = compiler;

    public string Compile(Query query) => _compiler.Compile(query).ToString();
}