using Col.Domain.V1.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Col.Domain.V1;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCollection(this IServiceCollection services)
    {
        services.AddSingleton<SqlQueries>();
        services.AddScoped<ICollectionRepository, CollectionRepository>();
        
        return services;
    }
}