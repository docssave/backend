using FluentValidation;
using Idn.Contracts;
using Idn.Domain;
using Idn.Plugin.V1.Validators;
using MediatR;

namespace Idn.Plugin.V1;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityService(this IServiceCollection services)
    {
        return services
            .AddMediatR(typeof(MediatorEntryPoint).Assembly)
            .AddScoped<IUserIdAccessor, UserIdAccessor>()
            .AddSingleton<IValidator<AuthorizationRequest>, AuthorizationRequestValidator>()
            .AddIdentity();
    }
}