namespace RpgBooks.Libraries.Module.Application.Commands;

using Microsoft.Extensions.Logging;

internal static partial class SingleCommandHandlerDispatcherLogs
{
    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Warning,
        Message = "{CommandName} request validation failed",
        SkipEnabledCheck = true)]
    public static partial void LogRequestValidationFailed(this ILogger logger, string commandName);

    [LoggerMessage(
        EventId = 2,
        Level = LogLevel.Information,
        Message = "{CommandName} command request handled in {ElapsedMilliseconds}ms")]
    public static partial void LogRequestHandlingTime(this ILogger logger, string commandName, long elapsedMilliseconds);
}
