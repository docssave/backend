using SqlKata;

namespace Sql.Abstractions;

public interface IQueryCompiler
{
    string Compile(Query query);
}