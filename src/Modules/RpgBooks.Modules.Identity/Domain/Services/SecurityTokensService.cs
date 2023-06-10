namespace RpgBooks.Modules.Identity.Domain.Services;

using RpgBooks.Libraries.Module.Application.Settings;
using RpgBooks.Modules.Identity.Application.Repositories.User;
using RpgBooks.Modules.Identity.Application.Repositories.User.Model;
using RpgBooks.Modules.Identity.Domain.Entities;
using RpgBooks.Modules.Identity.Domain.Services.Abstractions;
using RpgBooks.Modules.Identity.Domain.Settings;

/// <summary>
/// Service responsible for generating and managing user security tokens.
/// </summary>
internal sealed class SecurityTokensService : ISecurityTokensService
{
    private readonly ApplicationSecrets secrets;
    private readonly IdentitySettings identitySettings;
    private readonly IUserReadOnlyRepository userReadOnlyRepository;

    /// <summary>
    /// Creates a new instance of <see cref="SecurityTokensService"/>.
    /// </summary>
    /// <param name="userReadOnlyRepository">User read only repository.</param>
    /// <param name="appSecrets">Application secrets options.</param>
    /// <param name="identitySettings">Identity module settings</param>
    public SecurityTokensService(
        IUserReadOnlyRepository userReadOnlyRepository,
        IOptions<ApplicationSecrets> appSecrets,
        IOptions<IdentitySettings> identitySettings)
    {
        this.secrets = appSecrets.Value;
        this.identitySettings = identitySettings.Value;
        this.userReadOnlyRepository = userReadOnlyRepository;
    }

    /// <inheritdoc/>
    public ValueTask<TokenModel> GenerateEmailConfirmationToken(User user, CancellationToken cancellation = default)
    {
        var type = SecurityTokenType.ConfirmEmail;
        TimeSpan validity = TimeSpan.FromHours(
            this.identitySettings.SecurityTokenSettings.EmailConfirmationTokenValidityInHours);

        return GenerateToken(user, type, validity, null, cancellation);
    }

    /// <inheritdoc/>
    public ValueTask<TokenModel> GeneratePhoneConfirmationToken(User user, CancellationToken cancellation = default)
    {
        string phoneToken = RandomTokenProvider.GenerateRandomDigitsToken(
            this.identitySettings.SecurityTokenSettings.PhoneConfirmationTokenLength);
        TimeSpan validity = TimeSpan.FromMinutes(
            this.identitySettings.SecurityTokenSettings.EmailConfirmationTokenValidityInHours);

        var token = new SecurityToken(
            phoneToken.Encrypt(this.secrets.TokenProtectionSecret),
            SecurityTokenType.ConfirmPhoneNumber,
            validity);

        user.AddToken(token);
        return ValueTask.FromResult(new TokenModel(phoneToken, DateTimeOffset.UtcNow + validity));
    }

    /// <inheritdoc/>
    public ValueTask<TokenModel> GenerateRefreshToken(User user, string? sessionId, CancellationToken cancellation = default)
    {
        var type = SecurityTokenType.RefreshAuthentication;
        TimeSpan validity = TimeSpan.FromDays(
            this.identitySettings.LoginSettings.RefreshTokenTimeSpanInDays);

        return GenerateToken(user, type, validity, sessionId, cancellation);
    }

    /// <inheritdoc/>
    public ValueTask<TokenModel> GenerateRefreshToken(User user, CancellationToken cancellation = default)
        => GenerateRefreshToken(user, null, cancellation);

    /// <inheritdoc/>
    public ValueTask<TokenModel> GenerateResetPasswordToken(User user, CancellationToken cancellation = default)
    {
        var type = SecurityTokenType.ResetPassword;
        TimeSpan validity = TimeSpan.FromMinutes(
            this.identitySettings.LoginSettings.AuthTokenTimeSpanInMinutes);

        return GenerateToken(user, type, validity, null, cancellation);
    }

    /// <inheritdoc/>
    public async Task<bool> DisproveEmailConfirmationToken(
        int userId, string confirmationToken, CancellationToken cancellation = default)
    {
        var actualToken = await this.userReadOnlyRepository
            .GetActualToken(userId, SecurityTokenType.ConfirmEmail, cancellation);

        return actualToken is null
            || actualToken.IsExpired
            || confirmationToken != actualToken.Value.Decrypt(this.secrets.TokenProtectionSecret);
    }

    /// <inheritdoc/>
    public async Task<bool> DisproveRefreshToken(
        int userId, string refreshToken, string? sessionId, CancellationToken cancellation = default)
    {
        var actualToken = await this.userReadOnlyRepository
            .GetActualToken(userId, SecurityTokenType.RefreshAuthentication, sessionId, cancellation);
        string actualRefreshToken = actualToken?.Value.Decrypt(this.secrets.TokenProtectionSecret) ?? string.Empty;

        return actualToken is null
            || actualToken.IsExpired
            || refreshToken != actualRefreshToken;
    }

    /// <inheritdoc/>
    public async Task<bool> DisproveResetPasswordToken(
        int userId, string resetPasswordToken, CancellationToken cancellation = default)
    {
        var actualToken = await this.userReadOnlyRepository
            .GetActualToken(userId, SecurityTokenType.ResetPassword, cancellation);

        return actualToken is null
            || actualToken.IsExpired
            || resetPasswordToken != actualToken.Value.Decrypt(this.secrets.TokenProtectionSecret);
    }

    private ValueTask<TokenModel> GenerateToken(User user, SecurityTokenType tokenType, TimeSpan validity, string? sessionId, CancellationToken cancellation)
    {
        string tokenValue = RandomTokenProvider.GenerateRandomToken();
        var token = new SecurityToken(
            tokenValue.Encrypt(this.secrets.TokenProtectionSecret),
            tokenType,
            validity,
            sessionId);

        user.AddToken(token);
        return ValueTask.FromResult(new TokenModel(tokenValue, DateTimeOffset.UtcNow + validity));
    }
}
