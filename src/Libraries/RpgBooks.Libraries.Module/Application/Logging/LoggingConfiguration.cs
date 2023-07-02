namespace RpgBooks.Libraries.Module.Application.Logging;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

using System.Configuration;

/// <summary>
/// Logging configuration.
/// </summary>
public static class LoggingConfiguration
{
    /// <summary>
    /// Add Zero allocations logging.
    /// </summary>
    /// <param name="services">Services collection instance.</param>
    /// <param name="configuration">Application configuration.</param>
    /// <returns>Configured services collection.</returns>
    public static IServiceCollection AddZLogging(this IServiceCollection services, IConfiguration configuration)
    {
        LogEventLevel logLevel = LoggingSettings.GetGlobalLogLevel(configuration);
        LogEventLevel microsofrLogLevel = LoggingSettings.GetMicrosoftLogLevel(configuration);

        var logger = new LoggerConfiguration()
            .MinimumLevel.Is(logLevel)
            .MinimumLevel.Override("Microsoft", microsofrLogLevel)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", logLevel)
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()
            .Enrich.WithEnvironmentName()
            .Enrich.WithEnvironmentUserName()
            .WriteTo.Console(outputTemplate: LoggingSettings.LogTemplate)
            .WriteTo.File(
                path: LoggingSettings.GetFilePath(configuration),
                flushToDiskInterval: LoggingSettings.GetFlushToDiskInterval(configuration),
                rollingInterval: RollingInterval.Day,
                formatter: new JsonFormatter())
            .CreateLogger();

        Log.Logger = logger;
        services.AddSingleton<Microsoft.Extensions.Logging.ILogger>(new SerilogLogger(logger));
        return services;
    }

    /// <summary>
    /// Create bootstrap logger.
    /// </summary>
    /// <returns></returns>
    public static ILogger CreateBootstrapLogger()
    {
          var logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()
            .Enrich.WithThreadName()
            .Enrich.WithEnvironmentName()
            .WriteTo.Console(
                restrictedToMinimumLevel: LogEventLevel.Information,
                outputTemplate: LoggingSettings.LogTemplate)
            .CreateLogger();

        Log.Logger = logger;

        return logger;
    }

    private static string GetLogLevelForConsole(Microsoft.Extensions.Logging.LogLevel logLevel)
        => logLevel switch
        {
            Microsoft.Extensions.Logging.LogLevel.Trace => LoggingSettings.TraceLevelShort,
            Microsoft.Extensions.Logging.LogLevel.Debug => LoggingSettings.DebugLevelShort,
            Microsoft.Extensions.Logging.LogLevel.Information => LoggingSettings.InformationLevelShort,
            Microsoft.Extensions.Logging.LogLevel.Warning => LoggingSettings.WarningLevelShort,
            Microsoft.Extensions.Logging.LogLevel.Error => LoggingSettings.ErrorLevelShort,
            Microsoft.Extensions.Logging.LogLevel.Critical => LoggingSettings.CriticalLevelShort,
            _ => LoggingSettings.InformationLevelShort
        };
}
