using Microsoft.Extensions.DependencyInjection;
using Tg.Domain;
using Tg.Domain.V1;

namespace Tg.Plugin.V1.Extensions;

public static class ServiceTagExtensions
{
    public static IServiceCollection AddTagService(this IServiceCollection services)
    {
        return services
            .AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(typeof(MediatorEntryPoint).Assembly))
            .AddTag();
    }
}