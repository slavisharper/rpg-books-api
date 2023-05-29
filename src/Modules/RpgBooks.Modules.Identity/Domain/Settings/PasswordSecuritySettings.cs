namespace RpgBooks.Modules.Identity.Domain.Settings;

using System.Security.Cryptography;

public sealed class PasswordSecuritySettings
{
    public int SaltSize { get; init; } = 64;

    public int Iterations { get; init; } = 24000;

    public HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA512;
}
