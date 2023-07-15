using Col.Domain;
using Col.Domain.V1;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Col.Plugin.V1.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCollectionService(this IServiceCollection services)
    {
        return services
            .AddMediatR(typeof(MediatorEntryPoint).Assembly)
            .AddCollection();
    }
}