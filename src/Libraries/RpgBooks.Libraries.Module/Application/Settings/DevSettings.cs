namespace RpgBooks.Libraries.Module.Application.Settings;

/// <summary>
/// Settings related to the development team.
/// </summary>
public sealed class DevSettings
{
    /// <summary>
    /// Gets crash report settings.
    /// </summary>
    public CrashReportSettings CrashReport { get; init; } = default!;

    /// <summary>
    /// Gets development team emails.
    /// </summary>
    public IEnumerable<string> TeamEmails { get; init; } = default!;
}

/// <summary>
/// Crash report settings.
/// </summary>
public sealed class CrashReportSettings
{
    /// <summary>
    /// Gets error report application sender email.
    /// </summary>
    public string SenderEmail { get; init; } = default!;

    /// <summary>
    /// Gets error report application sender email.
    /// </summary>
    public string SenderName { get; init; } = default!;

    /// <summary>
    /// Gets the error report subject format.
    /// </summary>
    public string ReportSubject { get; init; } = default!;
}

