namespace RpgBooks.Modules.Identity.Domain.Services.Abstractions;

using RpgBooks.Modules.Identity.Application.Repositories.User.Model;
using RpgBooks.Modules.Identity.Domain.Entities;

/// <summary>
/// Security tokens service.
/// </summary>
public interface ISecurityTokensService
{
    /// <summary>
    /// Generates and sets new email confirmation token.
    /// </summary>
    /// <param name="user">User instance.</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>Generated token data model.</returns>
    ValueTask<TokenModel> GenerateEmailConfirmationToken(User user, CancellationToken cancellation = default);

    /// <summary>
    /// Generates and sets new phone confirmation token.
    /// </summary>
    /// <param name="user">User instance.</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>Generated token data model.</returns>
    ValueTask<TokenModel> GeneratePhoneConfirmationToken(User user, CancellationToken cancellation = default);

    /// <summary>
    /// Generates and sets new password reset token.
    /// </summary>
    /// <param name="user">User instance.</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>Generated token data model.</returns>
    ValueTask<TokenModel> GenerateResetPasswordToken(User user, CancellationToken cancellation = default);

    /// <summary>
    /// Generates and sets new authentication refresh token.
    /// </summary>
    /// <param name="user">User instance.</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>Generated token data model.</returns>
    ValueTask<TokenModel> GenerateRefreshToken(User user, CancellationToken cancellation = default);

    /// <summary>
    /// Generates and sets new authentication refresh token. This refresh token is bound to a session.
    /// </summary>
    /// <param name="user">User instance.</param>
    /// <param name="sessionId">Session Id.</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>Generated token data model.</returns>
    ValueTask<TokenModel> GenerateRefreshToken(User user, string? sessionId, CancellationToken cancellation = default);

    /// <summary>
    /// Get the last refresh token for a user session.
    /// </summary>
    /// <param name="userId">User id.</param>
    /// <param name="sessionId">Session Id.</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>The last created refresh token for the given session id.</returns>
    ValueTask<SecurityTokenReadModel?> GetLastRefreshToken(int userId, string? sessionId, CancellationToken cancellation = default);

    /// <summary>
    /// Get the last email confirmation token that is valid and can be used for the given user.
    /// </summary>
    /// <param name="userId">User id.</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>The last created email confirmation token for the given user.</returns>
    ValueTask<SecurityTokenReadModel?> GetLastEmailConfirmationToken(int userId, CancellationToken cancellation = default);

    /// <summary>
    /// Get the last password reset token that is valid and can be used for the given user.
    /// </summary>
    /// <param name="userId">User id.</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>The last password reset token for the given user.</returns>
    ValueTask<SecurityTokenReadModel?> GetLastPasswordResetToken(int userId, CancellationToken cancellation = default);
}
