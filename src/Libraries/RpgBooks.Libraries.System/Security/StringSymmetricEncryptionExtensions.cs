namespace System.Security;

using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Helper methods for encrypting and decrypting data.
/// </summary>
public static class StringSymmetricEncryptionExtensions
{
    /// <summary>
    /// Encrypts string with given symmetric key.
    /// </summary>
    /// <param name="text">Text that will be encrypted.</param>
    /// <param name="key">Symmetric key.</param>
    /// <returns>Encrypted string value.</returns>
    public static string EncryptString(string text, string key)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(text);

        using SymmetricAlgorithm crypt = Aes.Create();
        crypt.Key = MD5.HashData(Encoding.UTF8.GetBytes(key));
        crypt.GenerateIV();
        crypt.Padding = PaddingMode.PKCS7;

        using ICryptoTransform encryptor = crypt.CreateEncryptor();
        using MemoryStream memoryStream = new MemoryStream();
        using CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(bytes, 0, bytes.Length);
        cryptoStream.FlushFinalBlock();

        byte[] byteCipherText = memoryStream.ToArray();
        string base64IV = Convert.ToBase64String(crypt.IV);
        string base64Ciphertext = Convert.ToBase64String(byteCipherText);

        return base64IV + "!" + base64Ciphertext;
    }

    /// <summary>
    /// Encrypt string instance with given symmetric key.
    /// </summary>
    /// <param name="text">Text that will be encrypted.</param>
    /// <param name="key">Symmetric key.</param>
    /// <returns>Encrypted string value.</returns>
    public static string Encrypt(this string text, string key)
        => EncryptString(text, key);

    /// <summary>
    /// Decrypt encrypted string.
    /// </summary>
    /// <param name="cipherText">Encrypted text.</param>
    /// <param name="key">Same symmetric key used for encryption.</param>
    /// <returns>Original decrypted string value.</returns>
    public static string DecryptString(string cipherText, string key)
    {
        string[] cypherParts = cipherText.Split('!');

        using SymmetricAlgorithm crypt = Aes.Create();
        crypt.IV = Convert.FromBase64String(cypherParts[0]);
        crypt.Key = MD5.HashData(Encoding.UTF8.GetBytes(key));
        crypt.Padding = PaddingMode.PKCS7;

        using ICryptoTransform decryptor = crypt.CreateDecryptor();
        byte[] byteCypherText = Convert.FromBase64String(cypherParts[1]);

        using MemoryStream memoryStream = new MemoryStream(byteCypherText);
        using CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        using StreamReader streamReader = new StreamReader(cryptoStream);

        string originalText = streamReader.ReadToEnd();
        return originalText;
    }

    /// <summary>
    /// Decrypt string instance with given symmetric key.
    /// </summary>
    /// <param name="text">Text that will be decrypted.</param>
    /// <param name="key">Symmetric key.</param>
    /// <returns>Original decrypted string value.</returns>
    public static string Decrypt(this string text, string key)
        => DecryptString(text, key);
}
