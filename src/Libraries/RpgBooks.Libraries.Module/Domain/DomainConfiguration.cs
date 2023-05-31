namespace RpgBooks.Libraries.Module.Domain;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using RpgBooks.Libraries.Module.Domain.Events;

using System.Reflection;

/// <summary>
/// Module configuration for the domain layer.
/// </summary>
public static class DomainConfiguration
{
    /// <summary>
    /// Add and configures the domain layer events.
    /// </summary>
    /// <param name="services">Application service collection.</param>
    /// <param name="handlerAssemblies">Assemblies containing the domain event handlers.</param>
    /// <returns>Configured </returns>
    public static IServiceCollection AddDomainEvents(this IServiceCollection services, params Assembly[] handlerAssemblies)
    {
        services
            .Scan(scan => scan
            .FromAssemblies(handlerAssemblies)
            .AddClasses(c => c.AssignableTo(typeof(IDomainEventHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.TryAddScoped<IDomainEventDispatcher, AsyncEventDispatcher>();

        return services;
    }
}
