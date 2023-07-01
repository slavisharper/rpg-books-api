namespace RpgBooks.Libraries.Module.Application.Logging;

using Microsoft.Extensions.Configuration;

using Serilog.Events;

/// <summary>
/// Parses the logging settings from the configuration.
/// </summary>
internal class LoggingSettings
{
    public const string LogTemplate =
        "[{Timestamp:HH:mm:ss} {Level:u4}] {EventId} {Message:j} {NewLine}{Properties}{NewLine}{Exception}{NewLine}";

    public const int DefaultFlushInterval = 60;

    public const string TraceLevel = "Trace";

    public const string TraceLevelShort = "TRCE";

    public const string DebugLevel = "Debug";

    public const string DebugLevelShort = "DEBG";

    public const string InformationLevel = "Information";

    public const string InformationLevelShort = "INFO";

    public const string WarningLevel = "Warning";

    public const string WarningLevelShort = "WARN";

    public const string ErrorLevel = "Error";

    public const string ErrorLevelShort = "EROR";

    public const string CriticalLevel = "Critical";

    public const string CriticalLevelShort = "FATL";

    public static string GetFilePath(IConfiguration configuration)
        => configuration["Logging:Settings:FilePath"] ?? "logs/log-.txt";

    public static LogEventLevel GetGlobalLogLevel(IConfiguration configuration)
    {
        string? level = configuration["Logging:LogLevel:Default"];
        return GetLogLevel(level);
    }

    public static LogEventLevel GetMicrosoftLogLevel(IConfiguration configuration)
    {
        string? level = configuration["Logging:LogLevel:Microsoft.AspNetCore"];
        return GetLogLevel(level);
    }

    public static TimeSpan GetFlushToDiskInterval(IConfiguration configuration)
    {
        bool parseSuccess = int.TryParse(configuration["Logging:Settings:FlushToDiskIntervalInSeconds"], out int interval);
        return TimeSpan.FromSeconds(parseSuccess ? interval : DefaultFlushInterval);
    }

    private static LogEventLevel GetLogLevel(string? level)
    {
        return level switch
        {
            TraceLevel => LogEventLevel.Verbose,
            DebugLevel => LogEventLevel.Debug,
            InformationLevel => LogEventLevel.Information,
            WarningLevel => LogEventLevel.Warning,
            ErrorLevel => LogEventLevel.Error,
            CriticalLevel => LogEventLevel.Fatal,
            _ => LogEventLevel.Information,
        };
    }
}
