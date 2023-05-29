namespace RpgBooks.Libraries.Module.Application.Services.CurrentUser;
/// <summary>
/// Current user service.
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// Gets a value indicating whether current request is authenticated.
    /// </summary>
    bool IsAuthenticated { get; }

    /// <summary>
    /// Gets the current user instance of the request.
    /// </summary>
    ICurrentUser? User { get; }

    /// <summary>
    /// Gets the user IP address.
    /// </summary>
    string? Ip { get; }

    /// <summary>
    /// Gets the user agent.
    /// </summary>
    string? UserAgent { get; }
}
