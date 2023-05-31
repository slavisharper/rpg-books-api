namespace RpgBooks.Libraries.Module.Infrastructure.Services.Dev;

using Cysharp.Text;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

using RpgBooks.Libraries.Module.Application.Services.Dev;
using RpgBooks.Libraries.Module.Application.Services.Email;
using RpgBooks.Libraries.Module.Application.Settings;

using System;
using System.Linq;
using System.Threading.Tasks;

using static DevTeamEmailNotificationConstants;

/// <summary>
/// Email notification service for the development team.
/// </summary>
public sealed class DevTeamEmailNotificationService : IDevTeamNotificationService
{
    private readonly DevSettings devSettings;
    private readonly IEmailSender emailSender;

    /// <summary>
    /// Initializes a new instance of the <see cref="DevTeamEmailNotificationService"/> class.
    /// </summary>
    /// <param name="emailSender">SMTP client.</param>
    /// <param name="devSettingsOptions">Developer settings.</param>
    public DevTeamEmailNotificationService(
        IEmailSender emailSender,
        IOptions<DevSettings> devSettingsOptions)
    {
        this.devSettings = devSettingsOptions.Value;
        this.emailSender = emailSender;
    }

    /// <inheritdoc/>
    public async Task SendCrashReport<TException>(TException ex, HttpContext? context, string? additionalMessage, CancellationToken cancellationToken)
        where TException : Exception
    {
        var subject = ZString.Format("{0} {1}", this.devSettings.CrashReport.ReportSubject, ex.Message);
        var body = await GetReportBody(ex, context, additionalMessage);

        await this.emailSender.SendEmailAsync(this.devSettings.TeamEmails, subject, body, cancellationToken);
    }

    private static async Task<string> GetReportBody<TException>(TException ex, HttpContext? context, string? additionalMessage)
        where TException : Exception
    {
        var requestData = await GetRequestData(context);
        var message = string.Format(AdditionalMessageTemplate, additionalMessage ?? string.Empty);
        var appData = GetApplicationData();
        var exceptionData = GetExceptionData(ex);
        var innerExceptionMessage = ex.InnerException?.Message ?? string.Empty;
        var innerExceptionData = ex.InnerException is not null ? GetExceptionData(ex.InnerException) : string.Empty;
        var body = string.Format(BodyTemplate, TemplateStyles, ex.Message, appData, requestData, exceptionData, innerExceptionMessage, innerExceptionData, message);
        return body;
    }

    private static string GetExceptionData<TException>(TException ex)
        where TException : Exception
    {
        var type = ex.GetType();
        return string.Format(ExceptionTemplate, ex.Message, ex.Source, type.Name, type.FullName, ex.StackTrace);
    }

    private static string GetApplicationData()
    {
        return ZString.Format(
            AppDataTemplate,
            AppDomain.CurrentDomain.FriendlyName,
            AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName,
            Environment.OSVersion.VersionString,
            AppDomain.CurrentDomain.BaseDirectory);
    }

    private static async Task<string> GetRequestData(HttpContext? context)
    {
        if (context is null)
        {
            return GetEmptyRequestSection();
        }

        string? user = context.User.Identity?.Name;
        string? claims = string.Join(string.Empty, context.User.Claims.Select(c => string.Format(CellRowTemplate, c.Type, c.Value)));
        string? headers = string.Join(string.Empty, context.Request.Headers.Select(h => string.Format(CellRowTemplate, h.Key, h.Value)));
        string? requestBody = await ReadRequestBody(context);
        string localAddress = ZString.Format("{0}:{1}", context.Connection.LocalIpAddress, context.Connection.LocalPort);
        string remoteAddress = ZString.Format("{0}:{1}", context.Connection.RemoteIpAddress, context.Connection.RemotePort);

        return ZString.Format(
            RequestTemplate,
            context.Request.Host.Value,
            context.Request.Path,
            context.Request.QueryString,
            context.Request.Method,
            context.Request.ContentType,
            context.Request.ContentLength,
            context.Request.Protocol,
            context.Request.IsHttps,
            headers,
            context.Connection.Id,
            localAddress,
            remoteAddress,
            user,
            claims,
            requestBody);
    }

    private static string GetEmptyRequestSection()
    {
        return ZString.Format(
            RequestTemplate,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty);
    }

    private static async Task<string> ReadRequestBody(HttpContext context)
    {
        context.Request.Body.Seek(0, SeekOrigin.Begin);

        using var reader = new StreamReader(context.Request.BodyReader.AsStream());
        return await reader.ReadToEndAsync();
    }
}
