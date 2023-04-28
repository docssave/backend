﻿using Microsoft.Extensions.DependencyInjection;
using Ws.DataAccess;

namespace Ws.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWorkspace(this IServiceCollection services)
    {
        services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
        
        return services;
    }
}