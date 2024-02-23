using Doc.Domain.V1.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Doc.Domain.V1;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDocument(this IServiceCollection services)
    {
        services.AddSingleton<SqlQueries>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IFileRepository, FileRepository>();
        
        return services;
    }
}