using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.DependencyInjection;

namespace Tag.Domain;

public static class ServiceTagExtensions
{
    public static IServiceTag AddIdentity(this IServiceTag services)
    {
        return services;
    }
    
}