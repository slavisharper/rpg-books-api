namespace RpgBooks.Modules.Identity.Domain.Settings;

using System.Security.Cryptography;

/// <summary>
/// Password security settings. This class is used for configuring password hashing.
/// </summary>
public sealed class PasswordSecuritySettings
{
    /// <summary>
    /// Gets the size of the salt used for password hashing.
    /// <para>This is also the size for the password hash. They should be the same.</para>
    /// <para>Default is 64 bytes.</para>
    /// </summary>
    public int SaltSize { get; init; } = 64;

    /// <summary>
    /// Gets the number of iterations used for password hashing.
    /// <para>Default is 24000.</para>
    /// </summary>
    public int Iterations { get; init; } = 24000;

    /// <summary>
    /// Gets the hash algorithm used for password hashing.
    /// <para>Default is SHA512.</para>
    /// </summary>
    public HashAlgorithmName HashAlgorithm => HashAlgorithmName.SHA512;
}
