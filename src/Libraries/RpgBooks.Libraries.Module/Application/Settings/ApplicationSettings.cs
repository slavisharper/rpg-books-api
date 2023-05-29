namespace RpgBooks.Libraries.Module.Application.Settings;

/// <summary>
/// Main application settings coming from app config.
/// </summary>
public sealed class ApplicationSettings
{
    /// <summary>
    /// Gets the application name.
    /// </summary>
    public string ApplicationName { get; init; } = default!;

    /// <summary>
    /// Gets API URL.
    /// </summary>
    public string ApplicationUrl { get; private set; } = default!;

    /// <summary>
    /// Gets API URL.
    /// </summary>
    public string ClientUrl { get; private set; } = default!;

    /// <summary>
    /// Gets admin role name.
    /// </summary>
    public string AdminRoleName { get; init; } = default!;

    /// <summary>
    /// Gets admin role name.
    /// </summary>
    public string DeveloperRoleName { get; init; } = default!;

    /// <summary>
    /// Gets admin email.
    /// </summary>
    public string[] AdminEmails { get; init; } = default!;

    /// <summary>
    /// Gets the application date settings.
    /// </summary>
    public ApplicationDateSettings DateSettings { get; init; } = new ApplicationDateSettings();
}
