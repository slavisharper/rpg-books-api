namespace RpgBooks.Modules.Identity.Tests.Mocks;

using RpgBooks.Modules.Identity.Domain.Services.Abstractions;

internal static class IPasswordHasherMockFactory
{
    public static Mock<IPasswordHasher> CreateValidMock()
    {
        var passwordHasherMock = new Mock<IPasswordHasher>();
        passwordHasherMock
            .Setup(p => p.HashPassword(It.IsAny<string>()))
            .Returns("hashed_password");

        return passwordHasherMock;
    }
}
