namespace Libraries.System.Tests.Security;

using global::System.Security;

public sealed class ByteArrayEncryptionExtensionsTests
{
    [Fact]
    public void Encrypt_WithValidData_ShouldEncryptIt()
    {
        // Arrange
        var data = RandomTokenProvider.GenerateRandomBytes();
        string key = "SecretEncryptionTestKey";

        // Act
        var result = data.Encrypt(key);

        // Assert
        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void Decrypt_WithValidData_ShouldDecryptIt()
    {
        // Arrange
        var data = RandomTokenProvider.GenerateRandomBytes();
        string key = "SecretEncryptionTestKey";
        var encryptedData = data.Encrypt(key);

        // Act
        var result = encryptedData.Decrypt(key);

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.SequenceEqual(data).Should().BeTrue();
    }
}
