namespace RpgBooks.Modules.Identity.Domain.Settings;

public sealed class SecurityTokenSettings
{
    public int EmailConfirmationTokenValidityInHours { get; init; }

    public int PhoneConfirmationTokenValidityInMinutes { get; init; }

    public int PhoneConfirmationTokenLength { get; init; }
}
