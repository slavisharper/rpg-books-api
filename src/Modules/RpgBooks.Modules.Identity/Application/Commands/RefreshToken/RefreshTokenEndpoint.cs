namespace RpgBooks.Modules.Identity.Application.Commands.RefreshToken;

using RpgBooks.Libraries.Module.Presentation.Endpoints;
using RpgBooks.Modules.Identity.Application.Commands.Login;

/// <summary>
/// Refresh the authentication token endpoint.
/// </summary>
public sealed class RefreshTokenEndpoint : ApiEndpoint<RefreshTokenCommand, LoginResponseModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshTokenEndpoint"/> class.
    /// </summary>
    public RefreshTokenEndpoint()
        : base(
            "/api/account/refresh-token",
            nameof(RefreshTokenEndpoint),
            "account",
            EndpointTypes.Post,
            EndpointDelegates.CreateCommandHandlerDelegate<RefreshTokenCommand, LoginResponseModel>())
    {
    }
}
