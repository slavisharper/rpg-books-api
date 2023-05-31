namespace RpgBooks.Libraries.Module.Infrastructure.Services.Email;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using RpgBooks.Libraries.Module.Application.Services.Email;
using RpgBooks.Libraries.Module.Application.Settings;

using SendGrid;
using SendGrid.Helpers.Mail;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Email sender implementation using SendGrid.
/// </summary>
public sealed class SendGridEmailSender : IEmailSender
{
    private readonly ISendGridClient client;
    private readonly IEmailTemplateRenderer renderer;
    private readonly ILogger<SendGridEmailSender> logger;
    private readonly EmailAddress from;


    /// <summary>
    /// Initializes a new instance of the <see cref="SendGridEmailSender"/> class.
    /// </summary>
    /// <param name="client">Send grid client service.</param>
    /// <param name="renderer">Email body template renderer.</param>
    /// <param name="logger">Application logger.</param>
    /// <param name="emailSettingsOptions">Email settings.</param>
    public SendGridEmailSender(
        ISendGridClient client,
        IEmailTemplateRenderer renderer,
        ILogger<SendGridEmailSender> logger,
        IOptions<EmailSettings> emailSettingsOptions)
    {
        this.client = client;
        this.renderer = renderer;
        this.logger = logger;
        this.from = new EmailAddress(
            emailSettingsOptions.Value.SenderAddress,
            emailSettingsOptions.Value.SenderName);
    }

    /// <summary>
    /// Sends an email.
    /// </summary>
    /// <typeparam name="TData">Type of the body model.</typeparam>
    /// <param name="emailModel">Email body data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the email was sent successfully, false otherwise.</returns>
    public async Task<bool> SendEmailAsync<TData>(EmailModel<TData> emailModel, CancellationToken cancellationToken = default)
    {
        var bodyHtml = await this.renderer.RenderAsync(emailModel.Data, cancellationToken);
        return await this.SendEmailAsync(
            emailModel.To,
            emailModel.Subject,
            bodyHtml,
            emailModel.Cc,
            emailModel.Bcc,
            emailModel.Attachments,
            cancellationToken);
    }

    /// <summary>
    /// Sends an email.
    /// </summary>
    /// <param name="to">Email recipient.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="bodyHtml">Email body.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the email was sent successfully, false otherwise.</returns>
    public async Task<bool> SendEmailAsync(string to, string subject, string bodyHtml, CancellationToken cancellationToken = default)
        => await this.SendEmailAsync(
            new string[] {to},
            subject,
            bodyHtml,
            null,
            null,
            null,
            cancellationToken);

    /// <summary>
    /// Sends an email.
    /// </summary>
    /// <param name="to">Email recipient.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="bodyHtml">Email body.</param>
    /// <param name="attachments">Email attachments.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the email was sent successfully, false otherwise.</returns>
    public async Task<bool> SendEmailAsync(string to, string subject, string bodyHtml, IEnumerable<EmailAttachment>? attachments = null, CancellationToken cancellationToken = default)
        => await this.SendEmailAsync(
            new string[] { to },
            subject,
            bodyHtml,
            null,
            null,
            attachments,
            cancellationToken);

    /// <summary>
    /// Sends an email.
    /// </summary>
    /// <param name="to">Email recipients.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="bodyHtml">Email body.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the email was sent successfully, false otherwise.</returns>
    public async Task<bool> SendEmailAsync(IEnumerable<string> to, string subject, string bodyHtml, CancellationToken cancellationToken = default)
        => await this.SendEmailAsync(
            to,
            subject,
            bodyHtml,
            null,
            null,
            null,
            cancellationToken);

    /// <summary>
    /// Sends an email.
    /// </summary>
    /// <param name="to">Email recipients.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="bodyHtml">Email body.</param>
    /// <param name="attachments">Email attachments.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the email was sent successfully, false otherwise.</returns>
    public async Task<bool> SendEmailAsync(IEnumerable<string> to, string subject, string bodyHtml, IEnumerable<EmailAttachment>? attachments = null, CancellationToken cancellationToken = default)
        => await this.SendEmailAsync(
            to,
            subject,
            bodyHtml,
            null,
            null,
            attachments,
            cancellationToken);

    /// <summary>
    /// Sends an email.
    /// </summary>
    /// <param name="to">Email recipient.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="bodyHtml">Email body.</param>
    /// <param name="cc">Email CC recipients.</param>
    /// <param name="bcc">Email BCC recipients.</param>
    /// <param name="attachments">Email attachments.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if the email was sent successfully, false otherwise.</returns>
    public async Task<bool> SendEmailAsync(
        IEnumerable<string> to,
        string subject,
        string bodyHtml,
        IEnumerable<string>? cc = null,
        IEnumerable<string>? bcc = null,
        IEnumerable<EmailAttachment>? attachments = null,
        CancellationToken cancellationToken = default)
    {
        var msg = new SendGridMessage();

        msg.From = this.from;
        msg.AddTos(to.Select(to => new EmailAddress(to)).ToList());
        msg.Subject = subject;
        msg.SetFrom(this.from);
        msg.HtmlContent = bodyHtml;
        msg.PlainTextContent = bodyHtml.StripHtmlTags();

        if (cc!.IsNotEmpty())
        {
            msg.AddCcs(cc?.Select(cc => new EmailAddress(cc)).ToList());
        }

        if (bcc!.IsNotEmpty())
        {
            msg.AddBccs(bcc?.Select(bcc => new EmailAddress(bcc)).ToList());
        }

        await AddAttachmentsAsync(msg, attachments, cancellationToken);
        var result = await this.client.SendEmailAsync(msg, cancellationToken);

        if (!result.IsSuccessStatusCode)
        {
            string resultBody = await result.Body.ReadAsStringAsync(cancellationToken);
            this.logger.LogError("Error sending emails to {to}", string.Join(",", to));
            this.logger.LogError("Response body from SendGrid is {body}", resultBody);
        }

        return result.IsSuccessStatusCode;
    }

    private static async Task AddAttachmentsAsync(SendGridMessage msg, IEnumerable<EmailAttachment>? attachments, CancellationToken cancellationToken)
    {
        if (attachments is null)
        {
            return;
        }

        if (attachments is not null && attachments.IsNotEmpty())
        {
            foreach (var attachment in attachments)
            {
                using var attachmentStream = attachment.GetContentAsStream();
                await msg.AddAttachmentAsync(
                    attachment.Name,
                    attachmentStream,
                    cancellationToken: cancellationToken);
            }
        }
    }
}
