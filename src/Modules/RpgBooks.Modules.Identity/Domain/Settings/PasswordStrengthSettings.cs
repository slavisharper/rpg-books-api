namespace RpgBooks.Modules.Identity.Domain.Settings;

/// <summary>
/// Password strength settings.
/// </summary>
public sealed class PasswordStrengthSettings
{
    /// <summary>
    /// Gets minimal password length required on registration.
    /// </summary>
    public int MinPasswordLength { get; init; }

    /// <summary>
    /// Gets maximum password length required on registration.
    /// </summary>
    public int MaxPasswordLength { get; init; }

    /// <summary>
    /// Gets a value indicating whether user password will require digit.
    /// </summary>
    public bool RequireDigit { get; init; }

    /// <summary>
    /// Gets a value indicating whether user password will require lower case character.
    /// </summary>
    public bool RequireLowercase { get; init; }

    /// <summary>
    /// Gets a value indicating whether user password will require non alphanumeric characters.
    /// </summary>
    public bool RequireNonAlphanumeric { get; init; }

    /// <summary>
    /// Gets a value indicating whether user password will require upper case character.
    /// </summary>
    public bool RequireUppercase { get; init; }
}
