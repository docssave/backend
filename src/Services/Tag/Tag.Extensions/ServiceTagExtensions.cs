using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Tag.Extensions;

public static class ServiceTagExtensions
{
    
    public static IServiceTag AddWorkspaceServiceTag(this IServiceTag services)
        {
            return services
                .AddMediatR(typeof(MediatorEntryPoint).Assembly)
                .AddWorkspace(services)
                .BuildServiceProvider();
        }
    
}