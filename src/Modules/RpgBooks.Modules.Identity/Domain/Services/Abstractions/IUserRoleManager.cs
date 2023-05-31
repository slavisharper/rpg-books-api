namespace RpgBooks.Modules.Identity.Domain.Services.Abstractions;

using RpgBooks.Modules.Identity.Domain.Entities;

/// <summary>
/// User role manager service.
/// </summary>
internal interface IUserRoleManager
{
    /// <summary>
    /// Adds a user to a role.
    /// </summary>
    /// <param name="user">User instance.</param>
    /// <param name="roleName">Role name.</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>Await-able task.</returns>
    Task AddToRole(User user, string roleName, CancellationToken cancellation = default);

    /// <summary>
    /// Adds a user to the admin role.
    /// </summary>
    /// <param name="user">User instance.</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>Await-able task.</returns>
    Task AddToAdminRole(User user, CancellationToken cancellation = default);

    /// <summary>
    /// Adds a user to the developer role.
    /// </summary>
    /// <param name="user">User instance.</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>Await-able task.</returns>
    Task AddToDevRole(User user, CancellationToken cancellation = default);
}
