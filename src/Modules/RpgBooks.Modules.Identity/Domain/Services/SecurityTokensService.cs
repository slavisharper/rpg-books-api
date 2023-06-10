namespace RpgBooks.Modules.Identity.Domain.Services;

using Microsoft.Extensions.Options;

using RpgBooks.Libraries.Module.Application.Settings;
using RpgBooks.Modules.Identity.Application.Repositories.User;
using RpgBooks.Modules.Identity.Application.Repositories.User.Model;
using RpgBooks.Modules.Identity.Domain.Entities;
using RpgBooks.Modules.Identity.Domain.Services.Abstractions;
using RpgBooks.Modules.Identity.Domain.Settings;

using System.Security;

/// <summary>
/// Service responsible for generating and managing user security tokens.
/// </summary>
internal sealed class SecurityTokensService : ISecurityTokensService
{
    private readonly ApplicationSecrets appSecrets;
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
        this.appSecrets = appSecrets.Value;
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
            phoneToken.Encrypt(this.appSecrets.TokenProtectionSecret),
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
    public ValueTask<SecurityTokenReadModel?> GetLastEmailConfirmationToken(int userId, CancellationToken cancellation = default)
        => this.userReadOnlyRepository.GetActualToken(userId, SecurityTokenType.ConfirmEmail, cancellation);

    /// <inheritdoc/>
    public ValueTask<SecurityTokenReadModel?> GetLastPasswordResetToken(int userId, CancellationToken cancellation = default)
        => this.userReadOnlyRepository.GetActualToken(userId, SecurityTokenType.ResetPassword, cancellation);

    /// <inheritdoc/>
    public ValueTask<SecurityTokenReadModel?> GetLastRefreshToken(int userId, string? sessionId, CancellationToken cancellation = default)
        => this.userReadOnlyRepository.GetActualToken(userId, SecurityTokenType.ResetPassword, sessionId, cancellation);

    private ValueTask<TokenModel> GenerateToken(User user, SecurityTokenType tokenType, TimeSpan validity, string? sessionId, CancellationToken cancellation)
    {
        string tokenValue = RandomTokenProvider.GenerateRandomToken();
        var token = new SecurityToken(
            tokenValue.Encrypt(this.appSecrets.TokenProtectionSecret),
            tokenType,
            validity,
            sessionId);

        user.AddToken(token);
        return ValueTask.FromResult(new TokenModel(tokenValue, DateTimeOffset.UtcNow + validity));
    }
}
