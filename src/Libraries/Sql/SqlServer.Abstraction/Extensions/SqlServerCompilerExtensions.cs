using SqlKata;
using SqlKata.Compilers;

namespace SqlServer.Abstraction.Extensions;

public static class SqlServerCompilerExtensions
{
    public static string ToSqlQueryString(this SqlServerCompiler compiler, Query query) => compiler.Compile(query).ToString();
}