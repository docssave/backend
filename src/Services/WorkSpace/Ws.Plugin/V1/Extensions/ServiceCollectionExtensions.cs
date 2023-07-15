using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ws.Domain;
using Ws.Domain.V1;

namespace Ws.Plugin.V1.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWorkspaceService(this IServiceCollection services)
    {
        return services
            .AddMediatR(typeof(MediatorEntryPoint).Assembly)
            .AddWorkspace();
    }
}