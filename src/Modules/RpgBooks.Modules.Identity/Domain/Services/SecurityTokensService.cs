namespace RpgBooks.Modules.Identity.Domain.Services;

using RpgBooks.Libraries.Module.Application.Settings;
using RpgBooks.Modules.Identity.Domain.Entities;
using RpgBooks.Modules.Identity.Domain.Services.Abstractions;
using RpgBooks.Modules.Identity.Domain.Settings;

using Microsoft.Extensions.Options;

using System.Security;

public sealed class SecurityTokensService : ISecurityTokensService
{
    private readonly ApplicationSecrets appSecrets;
    private readonly IdentitySettings identitySettings;

    public SecurityTokensService(
        IOptions<ApplicationSecrets> appSecrets,
        IOptions<IdentitySettings> identitySettings)
    {
        this.appSecrets = appSecrets.Value;
        this.identitySettings = identitySettings.Value;
    }

    public ValueTask<TokenModel> GenerateEmailConfirmationToken(User user, CancellationToken cancellation = default)
    {
        var type = SecurityTokenType.ConfirmEmail;
        TimeSpan validity = TimeSpan.FromHours(
            this.identitySettings.SecurityTokenSettings.EmailConfirmationTokenValidityInHours);

        return GenerateToken(user, type, validity, cancellation);
    }

    public ValueTask<TokenModel> GeneratePhoneConfirmationToken(User user, CancellationToken cancellation = default)
    {
        string phoneToken = RandomTokenProvider.GenerateRandomDigitsToken(
            this.identitySettings.SecurityTokenSettings.PhoneConfirmationTokenLength);
        TimeSpan validity = TimeSpan.FromMinutes(
            this.identitySettings.SecurityTokenSettings.EmailConfirmationTokenValidityInHours);

        var token = new SecurityToken(
            phoneToken.Encrypt(this.appSecrets.TokenProtectionSecret),
            SecurityTokenType.ConfirmPhoneNumber,
            user,
            validity);

        user.AddToken(token);
        return ValueTask.FromResult(new TokenModel(phoneToken, DateTimeOffset.UtcNow + validity));
    }

    public ValueTask<TokenModel> GenerateRefreshToken(User user, CancellationToken cancellation = default)
    {
        var type = SecurityTokenType.RefreshAuthentication;
        TimeSpan validity = TimeSpan.FromDays(
            this.identitySettings.LoginSettings.RefreshTokenTimeSpanInDays);

        return GenerateToken(user, type, validity, cancellation);
    }

    public ValueTask<TokenModel> GenerateResetPasswordToken(User user, CancellationToken cancellation = default)
    {
        var type = SecurityTokenType.ResetPassword;
        TimeSpan validity = TimeSpan.FromMinutes(
            this.identitySettings.LoginSettings.AuthTokenTimeSpanInMinutes);

        return GenerateToken(user, type, validity, cancellation);
    }

    private ValueTask<TokenModel> GenerateToken(User user, SecurityTokenType tokenType, TimeSpan validity, CancellationToken cancellation)
    {
        string tokenValue = RandomTokenProvider.GenerateRandomToken();
        var token = new SecurityToken(
            tokenValue.Encrypt(this.appSecrets.TokenProtectionSecret),
            tokenType,
            user,
            validity);

        user.AddToken(token);
        return ValueTask.FromResult(new TokenModel(tokenValue, DateTimeOffset.UtcNow + validity));
    }
}
