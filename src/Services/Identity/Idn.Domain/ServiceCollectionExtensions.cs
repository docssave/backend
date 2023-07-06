using System.IdentityModel.Tokens.Jwt;
using Idn.DataAccess;
using Idn.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Idn.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddSingleton<SqlQueries>();
        services.AddScoped<IIdentityRepository, IdentityRepository>();
        services.AddScoped<ISourceService, DummySourceService>();
        services.AddScoped<IEncryptor, DummyEncryptor>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddSingleton<JwtSecurityTokenHandler>();
        
        return services;
    }
}