namespace RpgBooks.Libraries.Module.Infrastructure.Services.CurrentUser;

using RpgBooks.Libraries.Module.Application.Services.CurrentUser;
using RpgBooks.Libraries.Module.Application.Settings;

using System.Security.Claims;

/// <summary>
/// Holds information about the current user performing the request to the server.
/// </summary>
public sealed class CurrentUser : ICurrentUser
{
    private const string NoUserMessage = "This request does not have an authenticated user.";

    private readonly ClaimsPrincipal principal;
    private readonly ApplicationSettings appSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="CurrentUser"/> class.
    /// </summary>
    /// <param name="principal">Logged user principal.</param>
    /// <param name="appSettings">Application settings.</param>
    public CurrentUser(ClaimsPrincipal principal, ApplicationSettings appSettings)
    {
        if (!principal.Claims.Any())
        {
            throw new ArgumentException(NoUserMessage);
        }

        this.principal = principal;
        this.appSettings = appSettings;
    }

    /// <inheritdoc />
    public string Id => this.principal.FindFirstValue("uid")!;

    /// <inheritdoc />
    public string? FirstName => this.principal.FindFirstValue("given_name")!;

    /// <inheritdoc />
    public string? LastName => this.principal.FindFirstValue("family_name")!;

    /// <inheritdoc />
    public string Email => this.principal.FindFirstValue("email")!;

    /// <inheritdoc />
    public bool IsAdmin =>
        this.principal.IsInRole(this.appSettings.AdminRoleName) ||
        this.principal.Claims.Any(c => c.ValueType == ClaimTypes.Role && c.Value == this.appSettings.AdminRoleName);

    /// <inheritdoc />
    public bool Claims(Func<Claim, bool> action)
    {
        return this.principal.Claims.Any(action);
    }
}
