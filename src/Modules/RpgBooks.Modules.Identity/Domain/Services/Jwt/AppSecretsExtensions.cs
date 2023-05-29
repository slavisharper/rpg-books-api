namespace RpgBooks.Modules.Identity.Domain.Services.Jwt;

using RpgBooks.Libraries.Module.Application.Settings;

using System.Text;

/// <summary>
/// App secrets extension methods.
/// </summary>
public static class AppSecretsExtensions
{
    /// <summary>
    /// Get the authentication symmetric security key.
    /// </summary>
    /// <param name="secrets">Application secrets.</param>
    /// <returns>Fetched symmetric security key.</returns>
    public static byte[] GetAuthenticationSecurityKey(this ApplicationSecrets secrets)
        => Encoding.ASCII.GetBytes(secrets.AuthenticationSecret);
    
}
