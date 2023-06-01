namespace RpgBooks.Modules.Identity.Application.Commands.Register;

using RpgBooks.Libraries.Module.Presentation.Endpoints;

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
            "/account",
            nameof(RegisterEndpoint),
            "account",
            EndpointTypes.Post,
            EndpointDelegates.CreateCommandHandlerDelegate<RegisterCommand, RegisterResponseModel>())
    {
    }
}
