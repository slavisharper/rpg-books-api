namespace RpgBooks.Modules.Identity.Application.Commands.UpdateDetails;

using RpgBooks.Libraries.Module.Presentation.Endpoints;
using RpgBooks.Libraries.Module.Presentation.Endpoints.Attributes;
using RpgBooks.Modules.Identity.Application.Settings;

/// <summary>
/// Update user details endpoint.
/// </summary>
[AuthorizeEndpoint]
public sealed class UpdateDetailsEndpoint :  ApiEndpoint<UpdateDetailsComand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateDetailsEndpoint"/> class.
    /// </summary>
    public UpdateDetailsEndpoint()
        : base(
            IdentityApplicationSettings.BaseModulePath,
            nameof(UpdateDetailsEndpoint),
            IdentityApplicationSettings.ModuleTagName,
            EndpointTypes.Put,
            EndpointDelegates.CommonCommandHandlerDelegate<UpdateDetailsComand>())
    {
    }
}
