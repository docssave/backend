using Fl.Domain.V1;
using Microsoft.Extensions.DependencyInjection;

namespace Fl.Plugin.V1.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFileService(this IServiceCollection services)
    {
        services.AddFile();

        return services;
    }
}