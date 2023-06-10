namespace RpgBooks.Modules.Identity.Application.Commands.ResetPassword;

using RpgBooks.Libraries.Module.Presentation.Endpoints;

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
			"/api/account/reset-password",
			nameof(ResetPasswordEndpoint),
			"account",
			EndpointTypes.Put,
			EndpointDelegates.CreateCommandHandlerDelegate<ResetPasswordCommand>())
	{
	}
}
