using Sql.Abstractions;
using SqlKata;
using SqlKata.Compilers;

namespace MySql;

internal sealed class SqlQueryCompiler : IQueryCompiler
{
    private readonly MySqlCompiler _compiler;

    public SqlQueryCompiler()
    {
        _compiler = new MySqlCompiler();
    }
    
    public string Compile(Query query) => _compiler.Compile(query).ToString();
}