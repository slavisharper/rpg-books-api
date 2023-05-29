namespace RpgBooks.Libraries.Module.Application.Services.Email;

using System.Net.Mail;
using System.Text.Json.Serialization;

/// <summary>
/// Email model class containing all the data required to send an email.
/// </summary>
public static class EmailModel
{
    /// <summary>
    /// Creates a new instance of the <see cref="EmailModel"/> class.
    /// </summary>
    /// <typeparam name="T">Type of the template model.</typeparam>
    /// <param name="to">Receiver address.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="bodyData">Data model used for generating the email body.</param>
    /// <returns>Created email model.</returns>
    public static EmailModel<T> Create<T>(string to, string subject, T bodyData)
        where T : notnull
    {
        return new EmailModel<T>(to, subject, bodyData);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="EmailModel"/> class.
    /// </summary>
    /// <typeparam name="T">Type of the template model.</typeparam>
    /// <param name="to">Receiver address.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="bodyData">Data model used for generating the email body.</param>
    /// <param name="attachments">Email attachments.</param>
    /// <returns>Created email model.</returns>
    public static EmailModel<T> Create<T>(string to, string subject, T bodyData, params EmailAttachment[] attachments)
        where T : notnull
    {
        return new EmailModel<T>(to, subject, bodyData, attachments);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="EmailModel"/> class.
    /// </summary>
    /// <typeparam name="T">Type of the template model.</typeparam>
    /// <param name="to">Main receivers addresses.</param>
    /// <param name="cc">CC receivers addresses.</param>
    /// <param name="bcc">BCC receivers addresses.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="bodyData">Data model used for generating the email body.</param>
    /// <returns>Created email model.</returns>
    public static EmailModel<T> Create<T>(
        IEnumerable<string> to,
        IEnumerable<string>? cc,
        IEnumerable<string>? bcc,
        string subject,
        T bodyData)
            where T : notnull
    {
        return new EmailModel<T>(to, cc, bcc, subject, bodyData);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="EmailModel"/> class.
    /// </summary>
    /// <typeparam name="T">Type of the template model.</typeparam>
    /// <param name="to">Main receivers addresses.</param>
    /// <param name="cc">CC receivers addresses.</param>
    /// <param name="bcc">BCC receivers addresses.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="bodyData">Data model used for generating the email body.</param>
    /// <param name="attachments">Email attachments.</param>
    /// <returns>Created email model.</returns>
    public static EmailModel<T> Create<T>(
        IEnumerable<string> to,
        IEnumerable<string>? cc,
        IEnumerable<string>? bcc,
        string subject,
        T bodyData,
        params EmailAttachment[] attachments)
            where T : notnull
    {
        return new EmailModel<T>(to, cc, bcc, subject, bodyData, attachments);
    }
}

/// <summary>
/// Email model containing necessary data for sending email messages.
/// </summary>
public sealed class EmailModel<TModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailModel{TModel}"/> class.
    /// </summary>
    /// <param name="to">Receiver address.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="data">Data model name used for generating the email body.</param>
    internal EmailModel(
        string to,
        string subject,
        TModel data)
    {
        this.To = new List<string> { to };
        this.Subject = subject;
        this.Data = data;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailModel{TModel}"/> class.
    /// </summary>
    /// <param name="to">Receiver address.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="data">Data model name used for generating the email body.</param>
    /// <param name="attachments">Email attachments.</param>
    internal EmailModel(
        string to,
        string subject,
        TModel data,
        params EmailAttachment[] attachments)
        : this(to, subject, data)
    {
        this.Attachments = attachments;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailModel{TModel}"/> class.
    /// </summary>
    /// <param name="to">Main receivers addresses.</param>
    /// <param name="cc">CC receivers addresses.</param>
    /// <param name="bcc">BCC receivers addresses.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="data">Data model name used for generating the email body.</param>
    internal EmailModel(
        IEnumerable<string> to,
        IEnumerable<string>? cc,
        IEnumerable<string>? bcc,
        string subject,
        TModel data)
    {
        this.To = to;
        this.Cc = cc;
        this.Bcc = bcc;
        this.Subject = subject;
        this.Data = data;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailModel{TModel}"/> class.
    /// </summary>
    /// <param name="to">Main receivers addresses.</param>
    /// <param name="cc">CC receivers addresses.</param>
    /// <param name="bcc">BCC receivers addresses.</param>
    /// <param name="subject">Email subject.</param>
    /// <param name="data">Data model name used for generating the email body.</param>
    /// <param name="attachments">Email attachments.</param>
    internal EmailModel(
        IEnumerable<string> to,
        IEnumerable<string>? cc,
        IEnumerable<string>? bcc,
        string subject,
        TModel data,
        params EmailAttachment[] attachments)
            : this(to, cc, bcc, subject, data)
    {
        this.Attachments = attachments;
    }

    /// <summary>
    /// Gets main receivers addresses of the generated email.
    /// </summary>
    public IEnumerable<string> To { get; init; }

    /// <summary>
    /// Gets CC receivers addresses of the generated email.
    /// </summary>
    public IEnumerable<string>? Cc { get; init; }

    /// <summary>
    /// Gets BCC receivers addresses of the generated email.
    /// </summary>
    public IEnumerable<string>? Bcc { get; init; }

    /// <summary>
    /// Gets email subject.
    /// </summary>
    public string Subject { get; init; }

    /// <summary>
    /// Gets data used for generating the body of the email.
    /// </summary>
    public TModel Data { get; init; }

    /// <summary>
    /// Gets email attachments.
    /// </summary>
    public IEnumerable<EmailAttachment>? Attachments { get; init; }
}
