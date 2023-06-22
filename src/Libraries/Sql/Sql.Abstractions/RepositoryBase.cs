using SqlKata.Compilers;

namespace Sql.Abstractions;

public abstract class RepositoryBase
{
    protected RepositoryBase()
    {
        QueryCompiler = new SqlServerCompiler();
    }

    protected SqlServerCompiler QueryCompiler { get; }
}