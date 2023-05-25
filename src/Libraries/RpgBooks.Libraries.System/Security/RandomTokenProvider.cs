namespace System.Security;

using System.Security.Cryptography;

/// <summary>
/// Random token provider based on <see cref="RandomNumberGenerator"/>.
/// </summary>
public static class RandomTokenProvider
{
    public static byte[] GenerateRandomBytes(int byteArrayLength = 64)
    {
        using var random = RandomNumberGenerator.Create();
        byte[] randomNumberBytes = new byte[byteArrayLength];
        random.GetBytes(randomNumberBytes);
        return randomNumberBytes;
    }

    public static string GenerateRandomToken(int byteArrayLengtht = 64)
        => Convert.ToBase64String(GenerateRandomBytes(byteArrayLengtht));

    public static string GenerateRandomDigitsToken(int numberOfDigits)
    {
        int from = (int)Math.Pow(10, numberOfDigits - 1);
        int to = from * 10 - 1;

        return RandomNumberGenerator.GetInt32(from, to).ToString();
    }
}
