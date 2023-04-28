using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ws.Domain;

namespace Ws.Plugin;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWorkspaceService(this IServiceCollection services)
    {
        return services
            .AddMediatR(typeof(MediatorEntryPoint).Assembly)
            .AddWorkspace();
    }
}