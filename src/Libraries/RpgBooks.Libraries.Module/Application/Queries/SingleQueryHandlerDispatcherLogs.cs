namespace RpgBooks.Libraries.Module.Application.Queries;

using Microsoft.Extensions.Logging;

internal static partial class SingleQueryHandlerDispatcherLogs
{
    [LoggerMessage(
        EventId = 3,
        Level = LogLevel.Information,
        Message = "{QueryName} query request is handled in {ElapsedMilliseconds}ms")]
    public static partial void LogRequestHandlingTime(this ILogger logger, string queryName, long elapsedMilliseconds);
}
