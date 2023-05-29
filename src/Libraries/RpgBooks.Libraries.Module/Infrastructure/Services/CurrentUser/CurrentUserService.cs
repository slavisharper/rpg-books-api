namespace RpgBooks.Libraries.Module.Infrastructure.Services.CurrentUser;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

using RpgBooks.Libraries.Module.Application.Services.CurrentUser;
using RpgBooks.Libraries.Module.Application.Settings;

using System.Security.Claims;

/// <summary>
/// Holds information about the current user performing the request to the server.
/// </summary>
public sealed class CurrentUserService : ICurrentUserService
{
    private const char EmptySpace = ' ';
    private const string AuthorizeHeaderKey = "Authorization";
    private const string UserAgentKey = "User-Agent";

    private readonly ICurrentUser? currentUser;

    /// <summary>
    /// Initializes a new instance of the <see cref="CurrentUser"/> class.
    /// </summary>
    /// <param name="context">HTTP context.</param>
    /// <param name="appSettingsOptions">Application settings.</param>
    /// <param name="jwtDecoder">JWT decoder service.</param>
    public CurrentUserService(IHttpContextAccessor context, IOptions<ApplicationSettings> appSettingsOptions, IJwtDecoder jwtDecoder)
    {
        var user = GetUserPrincipal(context, jwtDecoder);
        if (user is not null)
        {
            this.currentUser = new CurrentUser(user, appSettingsOptions.Value);
        }

        this.Ip = context.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        this.UserAgent = context.HttpContext?.Request.Headers[UserAgentKey];
    }

    /// <inheritdoc/>
    public ICurrentUser? User => this.currentUser;

    /// <inheritdoc/>
    public bool IsAuthenticated => this.User is not null;

    /// <inheritdoc/>
    public string? Ip { get; }

    /// <inheritdoc/>
    public string? UserAgent { get; }

    private static ClaimsPrincipal? GetUserPrincipal(IHttpContextAccessor context, IJwtDecoder jwtDecoder)
    {
        var user = context.HttpContext?.User;
        if (user is null)
        {
            return null;
        }

        if (user.Claims.Any())
        {
            return user;
        }

        var authorizationHeader = context.HttpContext?.Request.Headers
            .Where(h => h.Key == AuthorizeHeaderKey)
            .Select(h => h.Value)
            .FirstOrDefault()
            .FirstOrDefault();

        if (string.IsNullOrEmpty(authorizationHeader))
        {
            return null;
        }

        string token = authorizationHeader.Split(EmptySpace, options: StringSplitOptions.RemoveEmptyEntries)[1];
        return jwtDecoder.Decode(token);
    }
}
