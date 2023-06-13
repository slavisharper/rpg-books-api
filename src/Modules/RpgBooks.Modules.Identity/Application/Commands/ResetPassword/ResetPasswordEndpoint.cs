namespace RpgBooks.Modules.Identity.Application.Commands.ResetPassword;

using RpgBooks.Libraries.Module.Presentation.Endpoints;
using RpgBooks.Modules.Identity.Application.Settings;

/// <summary>
/// Reset password API endpoint.
/// </summary>
public sealed class ResetPasswordEndpoint : ApiEndpoint<ResetPasswordCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResetPasswordEndpoint"/> class.
    /// </summary>
    public ResetPasswordEndpoint()
		:base(
            ZString.Format("{0}/reset-password", IdentityApplicationSettings.BaseModulePath),
			nameof(ResetPasswordEndpoint),
			IdentityApplicationSettings.ModuleTagName,
			EndpointTypes.Put,
			EndpointDelegates.CommonCommandHandlerDelegate<ResetPasswordCommand>())
	{
	}
}
