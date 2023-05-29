namespace RpgBooks.Libraries.Module.Application.Services.Email;

/// <summary>
/// Email sender contract for sending emails using SMTP protocol.
/// </summary>
public interface ISmtpEmailSender : IEmailSender, IDisposable
{
}
