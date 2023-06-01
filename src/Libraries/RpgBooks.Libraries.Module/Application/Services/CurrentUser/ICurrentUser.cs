namespace RpgBooks.Libraries.Module.Application.Services.CurrentUser;

using System;
using System.Security.Claims;

/// <summary>
/// Current user interface.
/// </summary>
public interface ICurrentUser
{
    /// <summary>
    /// Gets current user name.
    /// </summary>
    int Id { get; }

    /// <summary>
    /// Gets current user name.
    /// </summary>
    string? FirstName { get; }

    /// <summary>
    /// Gets current user name.
    /// </summary>
    string? LastName { get; }

    /// <summary>
    /// Gets current user name.
    /// </summary>
    string Email { get; }

    /// <summary>
    /// Gets a value indicating whether user has admin role.
    /// </summary>
    bool IsAdmin { get; }

    /// <summary>
    /// Check if current user matches given claim condition.
    /// </summary>
    /// <param name="action">Claim matching action.</param>
    /// <returns>Whether current user has any matching claim to the given condition.</returns>
    bool Claims(Func<Claim, bool> action);
}
