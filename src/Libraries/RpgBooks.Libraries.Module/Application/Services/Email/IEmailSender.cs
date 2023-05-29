namespace RpgBooks.Libraries.Module.Application.Services.Email;

/// <summary>
/// Send generated email with external service provider.
/// </summary>
public interface IEmailSender
{
    /// <summary>
    /// Send email with the provided model that will try to use for body generation.
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <param name="emailModel"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> SendEmailAsync<TData>(
        EmailModel<TData> emailModel,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Send email with the provided information using external service provider.
    /// </summary>
    /// <param name="to">Email main recipient.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name= "bodyHtml">Email body in HTML format.</param>
    /// <param name="cancellationToken">Cancellation token for the async operations.</param>
    /// <returns>Completed task indicating whether the email was sent successfully.</returns>
    Task<bool> SendEmailAsync(
        string to,
        string subject,
        string bodyHtml,
        CancellationToken cancellationToken = default);


    /// <summary>
    /// Send email with the provided information using external service provider.
    /// </summary>
    /// <param name="to">Email main recipient.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name= "bodyHtml">Email body in HTML format.</param>
    /// <param name="attachments">Email attachments.</param>
    /// <param name="cancellationToken">Cancellation token for the async operations.</param>
    /// <returns>Completed task indicating whether the email was sent successfully.</returns>
    Task<bool> SendEmailAsync(
        string to,
        string subject,
        string bodyHtml,
        IEnumerable<EmailAttachment>? attachments = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Send email with the provided information using external service provider.
    /// </summary>
    /// <param name="to">Email main recipient.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="bodyHtml">Email body in HTML format.</param>
    /// <param name="cancellationToken">Cancellation token for the async operations.</param>
    /// <returns>Completed task indicating whether the email was sent successfully.</returns>
    Task<bool> SendEmailAsync(
        IEnumerable<string> to,
        string subject,
        string bodyHtml,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Send email with the provided information using external service provider.
    /// </summary>
    /// <param name="to">Email main recipient.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="bodyHtml">Email body in HTML format.</param>
    /// <param name="attachments">Email attachments.</param>
    /// <param name="cancellationToken">Cancellation token for the async operations.</param>
    /// <returns>Completed task indicating whether the email was sent successfully.</returns>
    Task<bool> SendEmailAsync(
        IEnumerable<string> to,
        string subject,
        string bodyHtml,
        IEnumerable<EmailAttachment>? attachments = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Send email with the provided information using external service provider.
    /// </summary>
    /// <param name="to">Email main recipient.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="bodyHtml">Email body in HTML format.</param>
    /// <param name="cc">Email CC recipients.</param>
    /// <param name="bcc">Email BCC recipients.</param>
    /// <param name="attachments">Email attachments.</param>
    /// <param name="cancellationToken">Cancellation token for the async operations.</param>
    /// <returns>Completed task indicating whether the email was sent successfully.</returns>
    Task<bool> SendEmailAsync(
        IEnumerable<string> to,
        string subject,
        string bodyHtml,
        IEnumerable<string>? cc = null,
        IEnumerable<string>? bcc = null,
        IEnumerable<EmailAttachment>? attachments = null,
        CancellationToken cancellationToken = default);
}
