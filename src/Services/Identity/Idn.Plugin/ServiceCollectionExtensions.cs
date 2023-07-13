using FluentValidation;
using Idn.Contracts;
using Idn.Contracts.Validators;
using Idn.Domain;
using MediatR;

namespace Idn.Plugin;

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