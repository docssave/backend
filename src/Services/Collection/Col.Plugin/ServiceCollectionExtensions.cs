using Col.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Col.Plugin;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCollectionService(this IServiceCollection services)
    {
        return services
            .AddMediatR(typeof(MediatorEntryPoint).Assembly)
            .AddCollection();
    }
}