namespace RpgBooks.Modules.Identity.Domain.Services;

using RpgBooks.Libraries.Module.Application.Settings;
using RpgBooks.Modules.Identity.Domain.Services.Abstractions;
using RpgBooks.Modules.Identity.Domain.Settings;

using Microsoft.Extensions.Options;

using System;
using System.Security;
using System.Security.Cryptography;
using System.Text;

internal sealed class PasswordHasher : IPasswordHasher
{
    private readonly IdentitySettings settings;
    private readonly ApplicationSecrets appSecrets;

    public PasswordHasher(IOptions<IdentitySettings> settingsOptions, IOptions<ApplicationSecrets> appSecrets)
    {
        this.settings = settingsOptions.Value;
        this.appSecrets = appSecrets.Value;
    }

    public string HashPassword(string password)
    {
        Span<byte> salt = stackalloc byte[this.settings.PasswordSecuritySettings.SaltSize];
        Span<byte> hash = stackalloc byte[this.settings.PasswordSecuritySettings.SaltSize];
        Span<byte> saltedHash = stackalloc byte[this.settings.PasswordSecuritySettings.SaltSize * 2];

        // Generate random salt
        salt = RandomNumberGenerator.GetBytes(this.settings.PasswordSecuritySettings.SaltSize);

        // Hash the password with the salt
        hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            this.settings.PasswordSecuritySettings.Iterations,
            this.settings.PasswordSecuritySettings.HashAlgorithm,
            this.settings.PasswordSecuritySettings.SaltSize);

        // Combine the salt and the hash
        for (int h = 0, i = 0; h < salt.Length; h++)
        {
            saltedHash[i++] = salt[h];
            saltedHash[i++] = hash[h];
        }

        // Encrypt the combined salt and hash
        return Convert.ToBase64String(saltedHash.Encrypt(this.appSecrets.PasswordProtectionSecret));
    }

    public bool VerifyPassword(string password, string encryptedSaltedHash)
    {
        var saltedHashBytes = Convert.FromBase64String(encryptedSaltedHash);

        Span<byte> saltedHash = stackalloc byte[this.settings.PasswordSecuritySettings.SaltSize * 2];
        saltedHash = saltedHashBytes.Decrypt(this.appSecrets.PasswordProtectionSecret);

        Span<byte> salt = stackalloc byte[this.settings.PasswordSecuritySettings.SaltSize];
        Span<byte> hash = stackalloc byte[this.settings.PasswordSecuritySettings.SaltSize];
        Span<byte> matchingHash = stackalloc byte[this.settings.PasswordSecuritySettings.SaltSize];

        for (int i = 0, h = 0; i < saltedHash.Length; i++)
        {
            if (i % 2 == 0)
            {
                salt[h] = saltedHash[i];
            }
            else
            {
                hash[h] = saltedHash[i];
                h++;
            }
        }

        matchingHash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            this.settings.PasswordSecuritySettings.Iterations,
            this.settings.PasswordSecuritySettings.HashAlgorithm,
            this.settings.PasswordSecuritySettings.SaltSize);

        return hash.SequenceEqual(matchingHash);
    }
}
