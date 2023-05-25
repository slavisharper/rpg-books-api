namespace System.Security;

using System.Security.Cryptography;

using System.Text;

using static System.Runtime.InteropServices.JavaScript.JSType;

/// <summary>
/// Encryption helpers for byte arrays.
/// </summary>
public static class ByteArrayEncryptionExtensions
{
    /// <summary>
    /// Encrypts byte array with given symmetric key.
    /// </summary>
    /// <param name="bytes">Byte array that will be encrypted.</param>
    /// <param name="key">Symmetric key.</param>
    /// <returns>Encrypted bytes value.</returns>
    public static byte[] Encrypt(this byte[] bytes, string key)
    {
        ReadOnlySpan<byte> readOnlyBeats = bytes;
        return Encrypt(readOnlyBeats, key);
    }

    /// <summary>
    /// Encrypts byte array with given symmetric key.
    /// </summary>
    /// <param name="bytes">Byte array that will be encrypted.</param>
    /// <param name="key">Symmetric key.</param>
    /// <returns>Encrypted bytes value.</returns>
    public static byte[] Encrypt(this Span<byte> bytes, string key)
    {
        ReadOnlySpan<byte> readOnlyBeats = bytes;
        return Encrypt(readOnlyBeats, key);
    }

    /// <summary>
    /// Encrypts byte array with given symmetric key.
    /// </summary>
    /// <param name="bytes">Byte array that will be encrypted.</param>
    /// <param name="key">Symmetric key.</param>
    /// <returns>Encrypted bytes value.</returns>
    public static byte[] Encrypt(this ReadOnlySpan<byte> bytes, string key)
    {
        using SymmetricAlgorithm crypt = Aes.Create();

        crypt.Key = MD5.HashData(Encoding.UTF8.GetBytes(key));
        crypt.GenerateIV();
        crypt.Padding = PaddingMode.None;

        using ICryptoTransform encryptor = crypt.CreateEncryptor();
        using MemoryStream memoryStream = new MemoryStream();
        using CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(bytes);
        cryptoStream.FlushFinalBlock();

        byte[] encryptedData = memoryStream.ToArray();
        byte[] result = new byte[encryptedData.Length + crypt.IV.Length];

        Array.Copy(crypt.IV, 0, result, 0, crypt.IV.Length);
        Array.Copy(encryptedData, 0, result, crypt.IV.Length, encryptedData.Length);

        return result;
    }

    /// <summary>
    /// Decrypts byte array value with given symmetric key.
    /// </summary>
    /// <param name="bytes">Encrypted bytes.</param>
    /// <param name="key">Symmetric key.</param>
    /// <returns>Decrypted original bytes.</returns>
    public static byte[] Decrypt(this byte[] bytes, string key)
    {
        using SymmetricAlgorithm crypt = Aes.Create();

        crypt.Key = MD5.HashData(Encoding.UTF8.GetBytes(key));
        crypt.Padding = PaddingMode.None;

        byte[] iv = new byte[crypt.BlockSize / 8];
        byte[] encryptedData = new byte[bytes.Length - iv.Length];

        Array.Copy(bytes, 0, iv, 0, iv.Length);
        Array.Copy(bytes, iv.Length, encryptedData, 0, encryptedData.Length);

        crypt.IV = iv;

        using ICryptoTransform decryptor = crypt.CreateDecryptor();
        using MemoryStream memoryStream = new MemoryStream();
        using CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write);
        cryptoStream.Write(encryptedData, 0, encryptedData.Length);

        byte[] result = memoryStream.ToArray();
        return result;
    }
}
