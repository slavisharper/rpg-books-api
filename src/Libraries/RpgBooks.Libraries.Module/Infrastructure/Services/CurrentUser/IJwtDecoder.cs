namespace RpgBooks.Libraries.Module.Infrastructure.Services.CurrentUser;

using System.Security.Claims;

/// <summary>
/// JWT decoder service contract.
/// </summary>
public interface IJwtDecoder
{
    /// <summary>
    /// Decodes an JWT token.
    /// </summary>
    /// <param name="token">JWT token value</param>
    /// <returns>Decoded JWT token as <see cref="ClaimsPrincipal"/>.</returns>
    ClaimsPrincipal? Decode(string token);
}
