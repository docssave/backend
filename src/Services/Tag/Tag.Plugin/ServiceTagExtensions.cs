using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Tag.Domain;

namespace Tag.Extensions;

public static class ServiceTagExtensions
{
    
    public static IServiceCollection AddTagService(this IServiceCollection services)
    {
        return services
            .AddMediatR(typeof(MediatorEntryPoint).Assembly)
            .AddTags();
    }
    
}