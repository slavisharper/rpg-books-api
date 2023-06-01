namespace RpgBooks.Modules.Identity.Application.Queries.Details;

using RpgBooks.Libraries.Module.Presentation.Endpoints;
using RpgBooks.Libraries.Module.Presentation.Endpoints.Attributes;

/// <summary>
/// User details endpoint.
/// </summary>
[AuthorizeEndpoint]
public sealed class UserDetailsEndpoint : ApiEndpoint<UserDetailsQuery, UserDetailsResponseModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserDetailsEndpoint"/> class.
    /// </summary>
    public UserDetailsEndpoint()
        : base(
            "/api/account",
            nameof(UserDetailsEndpoint),
            "account",
            EndpointTypes.Get,
            EndpointDelegates.QueryHandlerDelegate<UserDetailsQuery, UserDetailsResponseModel>())
    {
    }
}
