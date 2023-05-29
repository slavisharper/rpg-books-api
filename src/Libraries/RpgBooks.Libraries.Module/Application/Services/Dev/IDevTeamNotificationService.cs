namespace RpgBooks.Libraries.Module.Application.Services.Dev;

using Microsoft.AspNetCore.Http;

/// <summary>
/// Email service for notifying the Dev team.
/// This works separately from the application email work flow.
/// </summary>
public interface IDevTeamNotificationService
{
    /// <summary>
    /// Sends crash report to given Dev team emails.
    /// </summary>
    /// <typeparam name="TException">Thrown exception to report.</typeparam>
    /// <param name="ex">Exception instance.</param>
    /// <param name="httpAccessor">Request  HTTP context.</param>
    /// <param name="additionalMessage">Additional description for the error.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Completed task.</returns>
    Task SendCrashReport<TException>(TException ex, HttpContext? httpAccessor, string? additionalMessage, CancellationToken cancellationToken)
        where TException : Exception;
}
