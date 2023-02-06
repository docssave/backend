using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace PostgreSql.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDbConnectionFactory(this IServiceCollection services, SqlOptions options)
    {
        services.AddSingleton<IDbConnectionFactory>(_ =>
        {
            var connectionBuilder = new NpgsqlConnectionStringBuilder(options.ConnectionString);

            return new DbConnectionFactory(connectionBuilder);
        });

        return services;
    }
}