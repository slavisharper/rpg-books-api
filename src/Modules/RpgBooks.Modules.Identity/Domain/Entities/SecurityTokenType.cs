namespace RpgBooks.Modules.Identity.Domain.Entities;

using RpgBooks.Libraries.Module.Domain.Entities;

using System.Reflection;

/// <summary>
/// Security token types enumeration.
/// </summary>
public sealed class SecurityTokenType : Enumeration
{
    private SecurityTokenType(int value)
        : base(value, FromValue<SecurityTokenType>(value).Name)
    {
    }

    private SecurityTokenType(int value, string name)
        : base(value, name)
    {
    }

    /// <summary>
    /// Gets the token type responsible for verifying a phone number.
    /// </summary>
    public static readonly SecurityTokenType ConfirmPhoneNumber = new(1, nameof(ConfirmPhoneNumber));

    /// <summary>
    /// Gets the token type responsible for verifying an email address.
    /// </summary>
    public static readonly SecurityTokenType ConfirmEmail = new(2, nameof(ConfirmEmail));

    /// <summary>
    /// Gets the token type responsible for refreshing an authentication.
    /// </summary>
    public static readonly SecurityTokenType RefreshAuthentication = new(2, nameof(RefreshAuthentication));

    /// <summary>
    /// Gets the token type responsible for resetting a password.
    /// </summary>
    public static readonly SecurityTokenType ResetPassword = new(2, nameof(ResetPassword));
}
