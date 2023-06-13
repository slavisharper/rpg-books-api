namespace RpgBooks.Modules.Identity.Application.Commands.ChangePassword;

using RpgBooks.Libraries.Module.Presentation.Endpoints;
using RpgBooks.Libraries.Module.Presentation.Endpoints.Attributes;
using RpgBooks.Modules.Identity.Application.Settings;

/// <summary>
/// Change password endpoint.
/// </summary>
[AuthorizeEndpoint]
public sealed class ChangePasswordEndpoint : ApiEndpoint<ChangePasswordCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ChangePasswordEndpoint"/> class.
    /// </summary>
    public ChangePasswordEndpoint()
        : base(
            ZString.Format("{0}/change-password", IdentityApplicationSettings.BaseModulePath),
            nameof(ChangePasswordEndpoint),
            IdentityApplicationSettings.ModuleTagName,
            EndpointTypes.Put,
            EndpointDelegates.CommonCommandHandlerDelegate<ChangePasswordCommand>())
    {
    }
}
