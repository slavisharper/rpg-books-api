namespace RpgBooks.Libraries.Module.Application.Settings;

/// <summary>
/// Shared application date format settings.
/// </summary>
public sealed record ApplicationDateSettings
{
    /// <summary>
    /// Gets the application default date template.
    /// </summary>
    private const string DefaultGlobalDateTemplate = "dd MMM yyyy";

    /// <summary>
    /// Gets the application default date time template.
    /// </summary>
    private const string DefaultGlobalDateTimeTemplate = "dd MMM yyyy HH:mm:ss";

    /// <summary>
    /// Gets the application response default date time template.
    /// </summary>
    private const string DefaultApiResponseDateTemplate = "u";

    /// <summary>
    /// Gets or sets the application date template.
    /// </summary>
    public string GlobalDateTemplate { get; init; } = DefaultGlobalDateTemplate;

    /// <summary>
    /// Gets or sets the application date time template.
    /// </summary>
    public string GlobalDateTimeTemplate { get; init; } = DefaultGlobalDateTimeTemplate;

    /// <summary>
    /// Gets or sets the application response date time template.
    /// </summary>
    public string ApiResponseDateTemplate { get; init; } = DefaultApiResponseDateTemplate;
}
