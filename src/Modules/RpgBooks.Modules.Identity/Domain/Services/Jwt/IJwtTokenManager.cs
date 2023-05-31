namespace RpgBooks.Modules.Identity.Domain.Services.Jwt;

using RpgBooks.Modules.Identity.Domain.Entities;
using System.Security.Claims;

/// <summary>
/// JWT token manager service interface.
/// </summary>
public interface IJwtTokenManager
{
    /// <summary>
    /// Generate JWT token for the given user.
    /// </summary>
    /// <param name="user">User instance.</param>
    /// <param name="sessionId">Persistent session id when refreshing t.</param>
    /// <returns>Generated JWT token value.</returns>
    string GenerateToken(User user, string? sessionId = null);

    /// <summary>
    /// Reads and decodes JWT token.
    /// </summary>
    /// <param name="token">Token value.</param>
    /// <returns>
    /// <see cref="JwtPayload"/> object with all of the token information.
    /// <para>Returns null if the read was not successful.</para>
    /// </returns>
    JwtPayload? ReadToken(string token);
}
