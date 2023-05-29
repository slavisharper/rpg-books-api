namespace RpgBooks.Libraries.Module.Presentation;

using Microsoft.Extensions.DependencyInjection;

using RpgBooks.Libraries.Module.Presentation.Endpoints.Abstractions;

using System.Reflection;

/// <summary>
/// API endpoints configuration.
/// </summary>
public static class Configuration
{
    /// <summary>
    /// Configure API endpoints.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="contextAssemblies"></param>
    /// <returns></returns>
    public static IServiceCollection AddApiEndpoints(
        this IServiceCollection services, params Assembly[] contextAssemblies)
    {
        foreach (var assembly in contextAssemblies)
        {
            services.Scan(scan => scan
                    .FromAssemblies(assembly)
                    .AddClasses(classes => classes
                        .AssignableTo(typeof(IApiEndpoint)))
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime());
        }

        return services;
    }
}
