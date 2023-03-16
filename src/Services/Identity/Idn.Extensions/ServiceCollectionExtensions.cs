using Idn.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Idn.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityService(this IServiceCollection services)
    {
        services.AddMediatR(typeof(MediatorEntryPoint).Assembly);
        
        return services;
    }
}