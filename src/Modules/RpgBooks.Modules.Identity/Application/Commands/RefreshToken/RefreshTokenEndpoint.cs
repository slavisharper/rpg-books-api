namespace RpgBooks.Modules.Identity.Application.Commands.RefreshToken;

using RpgBooks.Libraries.Module.Presentation.Endpoints;
using RpgBooks.Modules.Identity.Application.Commands.Login;
using RpgBooks.Modules.Identity.Application.Settings;

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
            ZString.Format("{0}/refresh-token", IdentityApplicationSettings.BaseModulePath),
            nameof(RefreshTokenEndpoint),
            IdentityApplicationSettings.ModuleTagName,
            EndpointTypes.Post,
            EndpointDelegates.CommonCommandHandlerDelegate<RefreshTokenCommand, LoginResponseModel>())
    {
    }
}
