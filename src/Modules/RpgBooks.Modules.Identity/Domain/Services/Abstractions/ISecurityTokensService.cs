namespace RpgBooks.Modules.Identity.Domain.Services.Abstractions;

using RpgBooks.Modules.Identity.Application.Repositories.User.Model;
using RpgBooks.Modules.Identity.Domain.Entities;

/// <summary>
/// Security tokens service.
/// </summary>
internal interface ISecurityTokensService
{
    /// <summary>
    /// Generates and sets new email confirmation token.
    /// </summary>
    /// <param name="user">User instance.</param>
    /// <returns>Generated token data model.</returns>
    TokenModel GenerateEmailConfirmationToken(User user);

    /// <summary>
    /// Generates and sets new phone confirmation token.
    /// </summary>
    /// <param name="user">User instance.</param>
    /// <returns>Generated token data model.</returns>
    TokenModel GeneratePhoneConfirmationToken(User user);

    /// <summary>
    /// Generates and sets new password reset token.
    /// </summary>
    /// <param name="user">User instance.</param>
    /// <returns>Generated token data model.</returns>
    TokenModel GenerateResetPasswordToken(User user);

    /// <summary>
    /// Generates and sets new authentication refresh token.
    /// </summary>
    /// <param name="user">User instance.</param>
    /// <returns>Generated token data model.</returns>
    TokenModel GenerateRefreshToken(User user);

    /// <summary>
    /// Generates and sets new authentication refresh token. This refresh token is bound to a session.
    /// </summary>
    /// <param name="user">User instance.</param>
    /// <param name="sessionId">Session Id.</param>
    /// <returns>Generated token data model.</returns>
    TokenModel GenerateRefreshToken(User user, string? sessionId);

    /// <summary>
    /// Checks if the given email confirmation token is invalid.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="confirmationToken">Email confirmation token.</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>True if the token is invalid.</returns>
    Task<bool> DisproveEmailConfirmationToken(int userId, string confirmationToken, CancellationToken cancellation = default);

    /// <summary>
    /// Checks if the given refresh token is invalid.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="refreshToken">Refresh token value.</param>
    /// <param name="sessionId">Session id to which the refresh token is related.</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>True if the token is invalid.</returns>
    Task<bool> DisproveRefreshToken(int userId, string refreshToken, string? sessionId, CancellationToken cancellation = default);

    /// <summary>
    /// Checks if the given reset password token is invalid.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="resetPasswordToken">Email confirmation token.</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>True if the token is invalid.</returns>
    Task<bool> DisproveResetPasswordToken(int userId, string resetPasswordToken, CancellationToken cancellation = default);
}
