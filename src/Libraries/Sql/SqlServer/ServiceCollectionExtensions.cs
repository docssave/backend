using Microsoft.Extensions.DependencyInjection;
using SqlServer.Abstraction;

namespace SqlServer;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDbConnectionFactory(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IDbConnectionFactory>(_ => new DbConnectionFactory(connectionString));

        return services;
    }
}