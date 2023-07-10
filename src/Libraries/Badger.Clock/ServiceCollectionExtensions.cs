using Microsoft.Extensions.DependencyInjection;

namespace Badger.Clock;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddClock(this IServiceCollection services)
    {
        services.AddSingleton<IClock, ServerClock>();
        
        return services;
    }
}