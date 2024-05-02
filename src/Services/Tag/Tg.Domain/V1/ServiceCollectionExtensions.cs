using Microsoft.Extensions.DependencyInjection;
using Tg.Domain.V1.DataAccess;

namespace Tg.Domain.V1;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTag(this IServiceCollection services)
    {
        services.AddSingleton<SqlQueries>();
        services.AddScoped<ITagRepository, TagRepository>();

        return services;
    }
}