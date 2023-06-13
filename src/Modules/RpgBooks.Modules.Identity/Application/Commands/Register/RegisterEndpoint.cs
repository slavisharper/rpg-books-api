namespace RpgBooks.Modules.Identity.Application.Commands.Register;

using RpgBooks.Libraries.Module.Presentation.Endpoints;
using RpgBooks.Modules.Identity.Application.Settings;

/// <summary>
/// Register user API endpoint.
/// </summary>
public sealed class RegisterEndpoint : ApiEndpoint<RegisterCommand, RegisterResponseModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterEndpoint"/> class.
    /// </summary>
    public RegisterEndpoint()
        : base(
            IdentityApplicationSettings.BaseModulePath,
            nameof(RegisterEndpoint),
            IdentityApplicationSettings.ModuleTagName,
            EndpointTypes.Post,
            EndpointDelegates.CommonCommandHandlerDelegate<RegisterCommand, RegisterResponseModel>())
    {
    }
}
