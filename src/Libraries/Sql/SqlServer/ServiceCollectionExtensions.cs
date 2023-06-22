using Microsoft.Extensions.DependencyInjection;
using SqlKata.Compilers;
using Sql.Abstractions;

namespace SqlServer;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDbConnectionFactory(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IDbConnectionFactory>(_ => new DbConnectionFactory(connectionString));
        services.AddSingleton<SqlServerCompiler>();
        services.AddSingleton<IQueryCompiler, SqlQueryCompiler>();

        return services;
    }
}