namespace RpgBooks.Modules.Identity.Application.Queries.Details;

using RpgBooks.Libraries.Module.Presentation.Endpoints;
using RpgBooks.Libraries.Module.Presentation.Endpoints.Attributes;
using RpgBooks.Modules.Identity.Application.Repositories.User.Model;

/// <summary>
/// User details endpoint.
/// </summary>
[AuthorizeEndpoint]
public sealed class UserDetailsEndpoint : ApiEndpoint<UserDetailsQuery, UserDetailsReadModel>
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
            EndpointDelegates.QueryHandlerDelegate<UserDetailsQuery, UserDetailsReadModel>())
    {
    }
}
