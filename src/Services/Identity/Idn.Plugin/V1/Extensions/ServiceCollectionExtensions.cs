using System.Text;
using FluentValidation;
using Idn.Contracts.V1;
using Idn.Domain;
using Idn.Domain.V1;
using Idn.Domain.V1.Options;
using Idn.Plugin.V1.Validators;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Idn.Plugin.V1.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityService(this IServiceCollection services, Func<string, IConfigurationSection> getSectionFunc)
    {
        var tokenOptions = getSectionFunc(TokenOptions.SectionName);

        services.AddAuthentication("Bearer")
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = tokenOptions["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = tokenOptions["Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenOptions["ClientSecret"])),
                    ValidateLifetime = false
                };
            });

        services.Configure<TokenOptions>(tokenOptions);
        
        return services
            .AddMediatR(typeof(MediatorEntryPoint).Assembly)
            .AddScoped<IUserIdAccessor, UserIdAccessor>()
            .AddSingleton<IValidator<AuthorizationRequest>, AuthorizationRequestValidator>()
            .AddIdentity();
    }
}