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
    private static readonly Dictionary<LogLevel, LogEventLevel> LogEventLevelMapping = new()
    {
        { LogLevel.Trace, LogEventLevel.Verbose },
        { LogLevel.Debug, LogEventLevel.Debug },
        { LogLevel.Information, LogEventLevel.Information },
        { LogLevel.Warning, LogEventLevel.Warning },
        { LogLevel.Error, LogEventLevel.Error },
        { LogLevel.Critical, LogEventLevel.Fatal },
        { LogLevel.None, LogEventLevel.Fatal },
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="SerilogLogger"/> class.
    /// </summary>
    /// <param name="logger">Serilog instance.</param>
    public SerilogLogger(Serilog.ILogger logger)
        => this.logger = logger;

    /// <inheritdoc/>
    public IDisposable? BeginScope<TState>(TState state)
        where TState : notnull
        => default;

     /// <inheritdoc/>
    public bool IsEnabled(LogLevel logLevel)
        => this.logger.IsEnabled(LogEventLevelMapping[logLevel]);

    /// <inheritdoc/>
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        var logValues = state as IReadOnlyList<KeyValuePair<string, object>>;
        if (logValues is null || logValues.Count == 1)
        {
            this.logger.Write(LogEventLevelMapping[logLevel], exception, formatter(state, exception));
            return;
        }

        string messageTemplate = string.Empty;
        var values = new object[logValues.Count - 1];

        for (int i = 0, j = 0; i < logValues.Count; i++)
        {
            if (logValues[i].Key == "{OriginalFormat}")
            {
                messageTemplate = logValues[i].Value.ToString()!;
                continue;
            }

            values[j++] = logValues[i].Value;
        }

        this.logger.Write(LogEventLevelMapping[logLevel], exception, messageTemplate!, values);
    }
}