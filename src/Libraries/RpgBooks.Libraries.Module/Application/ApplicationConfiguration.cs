namespace RpgBooks.Libraries.Module.Application;

using Cysharp.Text;

using FluentValidation;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

using RpgBooks.Libraries.Module.Application.Commands;
using RpgBooks.Libraries.Module.Application.Commands.Contracts;
using RpgBooks.Libraries.Module.Application.Logging;
using RpgBooks.Libraries.Module.Application.Queries;
using RpgBooks.Libraries.Module.Application.Queries.Contracts;
using RpgBooks.Libraries.Module.Application.Settings;

using Serilog;
using Serilog.Events;

using System.Linq;
using System.Reflection;

/// <summary>
/// Application layer configurations.
/// </summary>
public static class ApplicationConfiguration
{
    /// <summary>
    /// Gets new service scope.
    /// </summary>
    /// <param name="services">Application services collection.</param>
    /// <returns>Service scope instance. NOTE: This is an <see cref="IDisposable"/> object.</returns>
    public static IServiceScope GetNewServiceScope(this IServiceCollection services)
        => services
            .BuildServiceProvider()
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();
    
    /// <summary>
    /// Adds CQRS support.
    /// </summary>
    /// <param name="services">Services collection instance.</param>
    /// <param name="assemblies">CQRS assemblies.</param>
    /// <returns>Configured services collection.</returns>
    public static IServiceCollection AddCQRS(this IServiceCollection services, params Assembly[] assemblies)
    {
        services
            .AddQueries(assemblies)
            .AddCommands(assemblies)
            .AddValidatorsFromAssemblies(assemblies);

        var serviceNames = services.Select(s => s.ServiceType.FullName);
        return services;
    }

    /// <summary> 
    /// Register Application settings class.
    /// </summary>
    /// <param name="services">Application services.</param>
    /// <param name="configuration">Application configuration.</param>
    /// <returns>Configured application services.</returns>
    public static IServiceCollection AddApplicationSettings(this IServiceCollection services, IConfiguration configuration)
        => services
            .Configure<ApplicationSettings>(
                configuration.GetSection(nameof(ApplicationSettings)),
                config => config.BindNonPublicProperties = true)
            .Configure<DevSettings>(
                configuration.GetSection(nameof(DevSettings)),
                config => config.BindNonPublicProperties = true)
            .Configure<ApplicationSecrets>(
                configuration.GetSection(nameof(ApplicationSecrets)),
                config => config.BindNonPublicProperties = true)
            .Configure<EmailSettings>(
                configuration.GetSection(nameof(EmailSettings)),
                config => config.BindNonPublicProperties = true);

    private static IServiceCollection AddQueries(this IServiceCollection services, Assembly[] assemblies)
    {
        services.TryAddScoped<IQueryHandlerDispatcher, SingleQueryHandlerDispatcher>();

        var queryHandlerType = typeof(IQueryHandler<,>);
        services.Scan(selector =>
        {
            selector
                .FromAssemblies(assemblies)
                .AddClasses(filter =>
                {
                    filter.AssignableTo(queryHandlerType);
                })
                .AsImplementedInterfaces(t => t.Name == queryHandlerType.Name)
                .WithScopedLifetime();
        });

        return services;
    }

    private static IServiceCollection AddCommands(this IServiceCollection services, Assembly[] assemblies)
    {
        services.TryAddScoped<ICommandHandlerDispatcher, SingleCommandHandlerDispatcher>();

        var commandHandlerType = typeof(ICommandHandler<,>);
        services.Scan(selector =>
        {
            selector
                .FromAssemblies(assemblies)
                .AddClasses(filter =>
                {
                    filter.AssignableTo(commandHandlerType);
                })
                .AsImplementedInterfaces(t => t.Name == commandHandlerType.Name)
                .WithScopedLifetime();
        });

        return services;
    }
}