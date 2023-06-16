namespace RpgBooks.Modules.Identity.Tests.Mocks;

using Identity.Domain.Services;
using RpgBooks.Modules.Identity.Domain.Entities;
using RpgBooks.Modules.Identity.Domain.Services.Abstractions;

using System;

internal static class ISecurityTokensServiceMockFactory
{
    internal static Mock<ISecurityTokensService> CreateValidMock()
    {
        var token = new SecurityToken(Ulid.NewUlid().ToString(), SecurityTokenType.ConfirmEmail);
        var returnModel = new TokenModel(token.Value, token.ExpirationTime);
        var mock = new Mock<ISecurityTokensService>();
        mock.Setup(m => m.GenerateEmailConfirmationToken(It.IsAny<User>()))
            .Callback((User user, CancellationToken _) =>
            { 
                user.AddToken(token);
            })
            .Returns(returnModel);
            
        return mock;
    }
}
