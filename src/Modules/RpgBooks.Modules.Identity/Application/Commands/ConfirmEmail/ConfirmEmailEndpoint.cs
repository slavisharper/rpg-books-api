namespace RpgBooks.Modules.Identity.Application.Commands.ConfirmEmail;

using RpgBooks.Libraries.Module.Presentation.Endpoints;

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
			"/api/account/confirm-email",
			nameof(ConfirmEmailEndpoint),
			"account",
			EndpointTypes.Post,
			EndpointDelegates.CreateCommandHandlerDelegate<ConfirmEmailCommand>())
	{
	}
}
