namespace RpgBooks.Modules.Identity.Application.Commands.ConfirmEmail;

using RpgBooks.Libraries.Module.Presentation.Endpoints;
using RpgBooks.Modules.Identity.Application.Commands.ChangePassword;
using RpgBooks.Modules.Identity.Application.Settings;

/// <summary>
/// Confirm a user's email address endpoint.
/// </summary>
public sealed class ConfirmEmailEndpoint : ApiEndpoint<ConfirmEmailCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConfirmEmailEndpoint"/> class.
    /// </summary>
    public ConfirmEmailEndpoint()
		: base(
            ZString.Format("{0}/confirm-email", IdentityApplicationSettings.BaseModulePath),
			nameof(ConfirmEmailEndpoint),
            IdentityApplicationSettings.ModuleTagName,
			EndpointTypes.Put,
			EndpointDelegates.CommonCommandHandlerDelegate<ConfirmEmailCommand>())
	{
	}
}
