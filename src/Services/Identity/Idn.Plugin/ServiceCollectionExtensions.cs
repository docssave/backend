using Idn.Contracts;
using Idn.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Idn.Plugin;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityService(this IServiceCollection services)
    {
        return services
            .AddMediatR(typeof(MediatorEntryPoint).Assembly)
            .AddScoped<IUserIdAccessor, UserIdAccessor>()
            .AddIdentity();
    }
}