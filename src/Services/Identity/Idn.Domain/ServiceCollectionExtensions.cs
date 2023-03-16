using Idn.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Idn.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddTransient<IIdentityRepository, IdentityRepository>();
        
        return services;
    }
}