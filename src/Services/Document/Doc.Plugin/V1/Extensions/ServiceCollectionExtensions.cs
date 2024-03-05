using Doc.Domain;
using Doc.Domain.V1;
using Microsoft.Extensions.DependencyInjection;

namespace Doc.Plugin.V1.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDocumentService(this IServiceCollection services)
    {
        services
            .AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(MediatorEntryPoint).Assembly))
            .AddDocument();

        return services;
    }
}