namespace RpgBooks.Modules.Identity.Domain.Settings;

/// <summary>
/// Login settings.
/// <para>This is used for configuring lockout settings, token lifespan, etc.</para>
/// </summary>
public sealed class LoginSettings
{
    /// <summary>
    /// Gets maximum allowed login attempts before user is locked.
    /// </summary>
    public int MaxLoginAttempts { get; init; }

    /// <summary>
    /// Gets a value indicating whether user account should be locked on unsuccessful login.
    /// </summary>
    public bool LockoutUserAccounts { get; init; }

    /// <summary>
    /// Gets lockout time in minutes.
    /// </summary>
    public int LockoutTimeSpanInMinutes { get; init; }

    /// <summary>
    /// Gets a value indicating whether on user registration authentication token should be generated or not.
    /// </summary>
    public bool LoginOnRegistration { get; init; }

    /// <summary>
    /// Gets access token lifespan in minutes.
    /// </summary>
    public int AuthTokenTimeSpanInMinutes { get; init; }

    /// <summary>
    /// Gets refresh token lifespan in days.
    /// </summary>
    public int RefreshTokenTimeSpanInDays { get; init; }

    /// <summary>
    /// Gets valid issuer URL address. This is the server domain.
    /// </summary>
    public string ValidIssuer { get; init; } = default!;

    /// <summary>
    /// Gets valid audience URL address. This is the client domain.
    /// </summary>
    public string ValidAudience { get; init; } = default!;
}
