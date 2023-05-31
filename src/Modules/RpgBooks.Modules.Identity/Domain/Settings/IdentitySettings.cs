namespace RpgBooks.Modules.Identity.Domain.Settings;

/// <summary>
/// Identity module settings.
/// </summary>
public sealed class IdentitySettings
{
    /// <summary>
    /// Login settings.
    /// <para>Hold information for user lockout settings, JWT and refresh tokens timespan and additional JWT token settings.</para>
    /// </summary>
    public LoginSettings LoginSettings { get; init; } = default!;

    /// <summary>
    /// Security token settings.
    /// <para>Holds information about security tokens validity and length.</para>
    /// </summary>
    public SecurityTokenSettings SecurityTokenSettings { get; init; } = default!;

    /// <summary>
    /// Password security settings.
    /// <para>Holds information about the password hashing logic.</para>
    /// </summary>
    public PasswordSecuritySettings PasswordSecuritySettings { get; init; } = default!;

    /// <summary>
    /// Password strength settings.
    /// <para>Holds information about what should be included in a user password.</para>
    /// </summary>
    public PasswordStrengthSettings PasswordStrengthSettings { get; init; } = default!;
}
