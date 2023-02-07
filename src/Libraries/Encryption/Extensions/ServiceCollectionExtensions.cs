using Microsoft.Extensions.DependencyInjection;

namespace Encryption.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEncryptor(this IServiceCollection services)
    {
        services.AddSingleton<IEncryptor, Sha256Encryptor>();

        return services;
    }
}