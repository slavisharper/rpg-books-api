namespace RpgBooks.Libraries.Module.Application.Logging;

using Microsoft.Extensions.Logging;

using Serilog;
using Serilog.Events;

using System;

/// <summary>
/// Custom <see cref="Microsoft.Extensions.Logging.ILogger"/> that uses Serilog.
/// </summary>
public class SerilogLogger : Microsoft.Extensions.Logging.ILogger
{
    private readonly Serilog.ILogger logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="SerilogLogger"/> class.
    /// </summary>
    /// <param name="logger">Serilog instance.</param>
    public SerilogLogger(Serilog.ILogger logger)
    { 
        this.logger = logger;
    }

    /// <inheritdoc/>
    public IDisposable? BeginScope<TState>(TState state)
        where TState : notnull
        => default;

     /// <inheritdoc/>
    public bool IsEnabled(LogLevel logLevel)
    { 
        return this.logger.IsEnabled(LogLevelToLogEventLevel(logLevel));
    }

    /// <inheritdoc/>
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        this.logger.Write(LogLevelToLogEventLevel(logLevel), exception, state?.ToString() ?? string.Empty);
    }

    private LogEventLevel LogLevelToLogEventLevel(LogLevel loglevel)
    {
        switch (loglevel)
        {
            case Microsoft.Extensions.Logging.LogLevel.Debug:
                return LogEventLevel.Debug;
            case Microsoft.Extensions.Logging.LogLevel.Information:
                return LogEventLevel.Information;
            case Microsoft.Extensions.Logging.LogLevel.Warning:
                return LogEventLevel.Warning;
            case Microsoft.Extensions.Logging.LogLevel.Error:
                return LogEventLevel.Error;
            case Microsoft.Extensions.Logging.LogLevel.Critical:
                return LogEventLevel.Fatal;
            case Microsoft.Extensions.Logging.LogLevel.None:
                return LogEventLevel.Verbose;
            case Microsoft.Extensions.Logging.LogLevel.Trace:
                return LogEventLevel.Verbose;
        }
        return LogEventLevel.Verbose;
    }
}