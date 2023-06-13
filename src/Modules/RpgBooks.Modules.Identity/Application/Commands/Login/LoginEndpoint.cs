namespace RpgBooks.Modules.Identity.Application.Commands.Login;

using RpgBooks.Libraries.Module.Presentation.Endpoints;
using RpgBooks.Modules.Identity.Application.Settings;

/// <summary>
/// Login user API endpoint.
/// </summary>
public sealed class LoginEndpoint : ApiEndpoint<LoginCommand, LoginResponseModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LoginEndpoint"/> class.
    /// </summary>
    public LoginEndpoint()
        : base(
            ZString.Format("{0}/login", IdentityApplicationSettings.BaseModulePath),
            nameof(LoginEndpoint),
            IdentityApplicationSettings.ModuleTagName,
            EndpointTypes.Post,
            EndpointDelegates.CommonCommandHandlerDelegate<LoginCommand, LoginResponseModel>())
    {
    }
}