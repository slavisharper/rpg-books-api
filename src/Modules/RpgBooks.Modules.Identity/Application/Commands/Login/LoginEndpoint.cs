namespace RpgBooks.Modules.Identity.Application.Commands.Login;

using RpgBooks.Libraries.Module.Presentation.Endpoints;

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
            "/api/account/login",
            nameof(LoginEndpoint),
            "account",
            EndpointTypes.Post,
            EndpointDelegates.CreateCommandHandlerDelegate<LoginCommand, LoginResponseModel>())
    {
    }
}