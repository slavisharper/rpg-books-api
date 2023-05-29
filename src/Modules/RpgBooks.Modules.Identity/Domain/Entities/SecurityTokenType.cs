namespace RpgBooks.Modules.Identity.Domain.Entities;

using RpgBooks.Libraries.Module.Domain.Entities;

using System.Reflection;

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

    public static readonly SecurityTokenType ConfirmPhoneNumber = new(1, nameof(ConfirmPhoneNumber));

    public static readonly SecurityTokenType ConfirmEmail = new(2, nameof(ConfirmEmail));

    public static readonly SecurityTokenType RefreshAuthentication = new(2, nameof(RefreshAuthentication));

    public static readonly SecurityTokenType ResetPassword = new(2, nameof(ResetPassword));
}
