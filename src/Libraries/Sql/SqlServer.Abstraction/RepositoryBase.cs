using SqlKata.Compilers;

namespace SqlServer.Abstraction;

public abstract class RepositoryBase
{
    protected RepositoryBase()
    {
        QueryCompiler = new SqlServerCompiler();
    }

    protected SqlServerCompiler QueryCompiler { get; }
}