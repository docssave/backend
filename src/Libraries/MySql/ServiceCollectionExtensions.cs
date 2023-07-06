using Microsoft.Extensions.DependencyInjection;
using Sql.Abstractions;
using SqlKata.Compilers;

namespace MySql;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDbConnectionFactory(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IDbConnectionFactory>(_ => new DbConnectionFactory(connectionString));
        services.AddSingleton<MySqlCompiler>();
        services.AddSingleton<IQueryCompiler, SqlQueryCompiler>();

        return services;
    } 
}