namespace System.Security;

using System.Security.Cryptography;

/// <summary>
/// Random token provider based on <see cref="RandomNumberGenerator"/>.
/// </summary>
public static class RandomTokenProvider
{
    /// <summary>
    /// Generate random, cryptographically secure bytes.
    /// </summary>
    /// <param name="byteArrayLength">Length of the byte array.</param>
    /// <returns></returns>
    public static byte[] GenerateRandomBytes(int byteArrayLength = 64)
    {
        Span<byte> randomNumberBytes = stackalloc byte[byteArrayLength];

        using var random = RandomNumberGenerator.Create();
        random.GetBytes(randomNumberBytes);

        return randomNumberBytes.ToArray();
    }

    /// <summary>
    /// Generates a random token for the specified byte length.
    /// </summary>
    /// <param name="byteArrayLengtht">Byte array length that will be randomly populated.</param>
    /// <returns></returns>
    public static string GenerateRandomToken(int byteArrayLengtht = 64)
        => Convert.ToBase64String(GenerateRandomBytes(byteArrayLengtht));

    /// <summary>
    /// Generates token containing random digits.
    /// </summary>
    /// <param name="numberOfDigits">Length of the token.</param>
    /// <returns>String containing random digits.</returns>
    public static string GenerateRandomDigitsToken(int numberOfDigits)
    {
        int from = (int)Math.Pow(10, numberOfDigits - 1);
        int to = from * 10 - 1;

        return RandomNumberGenerator.GetInt32(from, to).ToString();
    }
}
