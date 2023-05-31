namespace RpgBooks.Libraries.Module.Application.Settings;

/// <summary>
/// Email settings.
/// </summary>
public sealed class EmailSettings
{
    /// <summary>
    /// Gets friendly name that will appear instead of the sender email address in mail clients.
    /// </summary>
    public string SenderName { get; init; } = default!;

    /// <summary>
    /// Gets email address from which all emails are sent.
    /// </summary>
    public string SenderAddress { get; init; } = default!;
}
