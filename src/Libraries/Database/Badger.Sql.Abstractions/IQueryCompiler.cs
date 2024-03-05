using SqlKata;

namespace Badger.Sql.Abstractions;

public interface IQueryCompiler
{
    string Compile(Query query);
}