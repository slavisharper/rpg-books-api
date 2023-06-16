namespace RpgBooks.Modules.Identity.Tests.Domain.Builders;

using RpgBooks.Libraries.Module.Infrastructure.Services;
using RpgBooks.Modules.Identity.Domain.Builders;
using RpgBooks.Modules.Identity.Domain.Entities;
using RpgBooks.Modules.Identity.Domain.Events;
using RpgBooks.Modules.Identity.Domain.Exceptions;
using RpgBooks.Modules.Identity.Domain.Services.Abstractions;

public sealed class UserBuilderTests
{
    [Fact]
    public void Build_WithValidData_ShouldCreateUser()
    {
        // Arrange
        var passwordHasher = IPasswordHasherMockFactory.CreateValidMock().Object;
        var securityTokenService = ISecurityTokensServiceMockFactory.CreateValidMock().Object;
        var httpUtilities = new HttpUtilities();

        var builder = new UserBuilder(passwordHasher, securityTokenService, httpUtilities);

        // Act
        var user = builder
            .WithEmail("test@email.com")
            .WithPassword("Test_password#1")
            .Build();

        // Assert
        user.Should().NotBeNull();
        user.Email.Value.Should().Be("test@email.com");
        user.PasswordHash.Should().NotBeEmpty();

        user.SecurityTokens.Should().NotBeNull();
        user.SecurityTokens.Where(t => t.TokenType.Value == SecurityTokenType.ConfirmEmail.Value)
            .ToList()
            .Should().HaveCount(1);

        user.Events.Should().NotBeNull();
        user.Events.Where(e => e is UserRegisteredEvent)
            .ToList()
            .Should().HaveCount(1);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("test@email.com", null)]
    [InlineData(null, "Test_password#1")]
    public void Build_WithInvalidData_ShouldThrow(string email, string password)
    {
        // Arrange
        var passwordHasher = IPasswordHasherMockFactory.CreateValidMock().Object;
        var securityTokenService = ISecurityTokensServiceMockFactory.CreateValidMock().Object;
        var httpUtilities = new HttpUtilities();

        var builder = new UserBuilder(passwordHasher, securityTokenService, httpUtilities)
            .WithEmail(email)
            .WithPassword(password);

        // Act
        var action = () => builder.Build();

        // Assert
        action.Should().Throw<InvalidUserException>();
    }
}
