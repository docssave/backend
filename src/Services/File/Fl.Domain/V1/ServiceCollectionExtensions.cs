using Fl.Domain.V1.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Fl.Domain.V1;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFiles(this IServiceCollection services)
    {
        services.AddSingleton<SqlQueries>();
        services.AddScoped<IFileRepository, FileRepository>();

        return services;
    }
}