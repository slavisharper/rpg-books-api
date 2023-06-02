namespace RpgBooks.Libraries.Module.Application;

using Cysharp.Text;

using FluentValidation;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

using RpgBooks.Libraries.Module.Application.Commands;
using RpgBooks.Libraries.Module.Application.Commands.Contracts;
using RpgBooks.Libraries.Module.Application.Queries;
using RpgBooks.Libraries.Module.Application.Queries.Contracts;
using RpgBooks.Libraries.Module.Application.Settings;

using System.Linq;
using System.Reflection;

using ZLogger;

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
    /// Add Zero allocations logging.
    /// </summary>
    /// <param name="services">Services collection instance.</param>
    /// <returns>Configured services collection.</returns>
    public static IServiceCollection AddZLogging(this IServiceCollection services)
        => services.AddLogging(config =>
        {
            config.ClearProviders();
            config.SetMinimumLevel(LogLevel.Information);

            config.AddZLoggerRollingFile(
                fileNameSelector: (dt, x) => ZString.Format("logs/{0:yyyy-MM-dd}_{1:000}.log", dt, x),
                timestampPattern: x => x.ToLocalTime().Date,
                rollSizeKB: 1024,
                options => 
                {
                    options.EnableStructuredLogging = true;
                });

            config.AddZLoggerConsole(options =>
            {
#if DEBUG
                // \u001b[31m => Red(ANSI Escape Code)
                // \u001b[0m => Reset
                // \u001b[38;5;***m => 256 Colors(08 is Gray)
                options.PrefixFormatter = (writer, info) =>
                {
                    string logLebel = GetLogLevelForConsole(info.LogLevel);
                    if (info.LogLevel == LogLevel.Error)
                    {
                        ZString.Utf8Format(writer, "\u001b[31m[{0} {1}] ", logLebel, info.Timestamp);
                    }
                    else if (info.LogLevel == LogLevel.Critical)
                    {
                        ZString.Utf8Format(writer, "\u001b[38;5;200m[{0} {1}] ", logLebel, info.Timestamp);
                    }
                    else
                    {
                        if (!info.CategoryName.StartsWith("Rpg")) // your application namespace.
                        {
                            if (info.LogLevel == LogLevel.Warning)
                            {
                                ZString.Utf8Format(writer, "\u001b[38;5;214m[{0} {1}] ", logLebel, info.Timestamp);
                            }
                            else
                            {
                                ZString.Utf8Format(writer, "\u001b[38;5;08m[{0} {1}] ", logLebel, info.Timestamp);
                            }
                        }
                        else
                        {
                            ZString.Utf8Format(writer, "[{0} {1}] ", logLebel, info.Timestamp);
                        }
                    }
                };
                options.SuffixFormatter = (writer, info) =>
                {
                    if (info.LogLevel == LogLevel.Error || !info.CategoryName.StartsWith("Rpg"))
                    {
                        ZString.Utf8Format(writer, "\u001b[0m", "");
                    }
                };
#endif
#if RELEASE
                // Tips: use PrepareUtf8 to achieve better performance.
                var prefixFormat = ZString.PrepareUtf8<LogLevel, DateTime>("[{0}] [{1}] ");
                options.PrefixFormatter = (writer, info) =>
                    prefixFormat.FormatTo(ref writer, info.LogLevel, info.Timestamp.DateTime.ToLocalTime());
#endif
            }, configureEnableAnsiEscapeCode: true);
        });
    
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

    private static string GetLogLevelForConsole(LogLevel logLevel)
        => logLevel switch
        {
            LogLevel.Trace => "TRCE",
            LogLevel.Debug => "DEBG",
            LogLevel.Information => "INFO",
            LogLevel.Warning => "WARN",
            LogLevel.Error => "EROR",
            LogLevel.Critical => "FATL",
            _ => "INFO"
        };
}