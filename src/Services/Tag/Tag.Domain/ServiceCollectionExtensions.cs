using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TagContracts;

namespace Tag.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTags(this IServiceCollection services)
    {
        services.AddSingleton<IValidator<CreateTagRequest>, CreateTagRequestValidator>();
        return services;
    }
}