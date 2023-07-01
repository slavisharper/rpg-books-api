namespace RpgBooks.Libraries.Module.Infrastructure.Services.Email;

using Microsoft.Extensions.Logging;

internal static partial class SendGridEmailSenderLogger
{
    [LoggerMessage(
        EventId = 5,
        Level = LogLevel.Error,
        Message = "Error sending email to {Email}",
        SkipEnabledCheck = true)]
    public static partial void LogEmailSendingFailure(this ILogger logger, string email);

    [LoggerMessage(
        EventId = 6,
        Level = LogLevel.Error,
        Message = "Response body from SendGrid is {response}",
        SkipEnabledCheck = true)]
    public static partial void LogEmailSendingFailureResponse(this ILogger logger, string response);
}
