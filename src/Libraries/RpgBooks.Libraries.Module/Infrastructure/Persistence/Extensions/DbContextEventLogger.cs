namespace RpgBooks.Libraries.Module.Infrastructure.Persistence.Extensions;

using Microsoft.Extensions.Logging;

internal static partial class DbContextEventLogger
{
    [LoggerMessage(
        EventId = 4,
        Level = LogLevel.Error,
        Message = "Event is poisoned and needs a review! {Event}",
        SkipEnabledCheck = true)]
    public static partial void LogPoisonedEvent(this ILogger logger, string Event);
}
