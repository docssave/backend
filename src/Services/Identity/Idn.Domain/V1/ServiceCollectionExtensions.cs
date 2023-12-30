using System.IdentityModel.Tokens.Jwt;
using Idn.Domain.V1.DataAccess;
using Idn.Domain.V1.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Idn.Domain.V1;

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