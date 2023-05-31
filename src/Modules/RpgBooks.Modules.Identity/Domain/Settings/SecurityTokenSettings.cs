namespace RpgBooks.Modules.Identity.Domain.Settings;

using RpgBooks.Modules.Identity.Domain.Entities;

/// <summary>
/// User security token settings.
/// <para>These settings are used for configuring <see cref="SecurityToken"/> and their lifespan, length, etc.</para>
/// </summary>
public sealed class SecurityTokenSettings
{
    /// <summary>
    /// Gets the email confirmation token validity in hours.
    /// </summary>
    public int EmailConfirmationTokenValidityInHours { get; init; }

    /// <summary>
    /// Gets the phone confirmation token validity in minutes.
    /// </summary>
    public int PhoneConfirmationTokenValidityInMinutes { get; init; }

    /// <summary>
    /// Gets the phone confirmation token length.
    /// </summary>
    public int PhoneConfirmationTokenLength { get; init; }
}
