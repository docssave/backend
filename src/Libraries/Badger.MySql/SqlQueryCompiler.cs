using Badger.Sql.Abstractions;
using Badger.SqlKata;
using SqlKata;

namespace Badger.MySql;

internal sealed class SqlQueryCompiler : IQueryCompiler
{
    private readonly ExtendedMySqlQueryCompiler _compiler;

    public SqlQueryCompiler()
    {
        _compiler = new ExtendedMySqlQueryCompiler();
    }
    
    public string Compile(Query query) => _compiler.Compile(query).ToString();
}