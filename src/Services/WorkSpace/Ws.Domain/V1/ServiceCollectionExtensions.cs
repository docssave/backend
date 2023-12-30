using Microsoft.Extensions.DependencyInjection;
using Ws.Domain.V1.DataAccess;

namespace Ws.Domain.V1;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWorkspace(this IServiceCollection services)
    {
        services.AddSingleton<SqlQueries>();
        services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();

        return services;
    }
}