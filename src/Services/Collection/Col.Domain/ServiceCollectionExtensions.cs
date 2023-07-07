using Col.Domain.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Col.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCollection(this IServiceCollection services)
    {
        services.AddSingleton<SqlQueries>();
        services.AddScoped<ICollectionRepository, CollectionRepository>();
        
        return services;
    }
}