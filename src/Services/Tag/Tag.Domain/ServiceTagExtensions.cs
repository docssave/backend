using Microsoft.Extensions.DependencyInjection;

namespace Tag.Domain;

public static class ServiceTagExtensions
{
    public static IServiceCollection AddTags(this IServiceCollection services)
    {
        return services;
    }
    
}