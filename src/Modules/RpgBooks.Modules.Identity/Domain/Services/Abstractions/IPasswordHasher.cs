namespace RpgBooks.Modules.Identity.Domain.Services.Abstractions;

/// <summary>
/// Password hasher service.
/// </summary>
internal interface IPasswordHasher
{
    /// <summary>
    /// Hashes a given password.
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    string HashPassword(string password);

    /// <summary>
    /// Verifies a given password against a hash.
    /// </summary>
    /// <param name="password">Password value to verify.</param>
    /// <param name="hash">Hash value to verify against.</param>
    /// <returns>true if the password matches the hash, otherwise false.</returns>
    bool VerifyPassword(string password, string hash);
}
