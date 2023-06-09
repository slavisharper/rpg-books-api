namespace RpgBooks.Modules.Identity.Domain.Repositories;

using RpgBooks.Libraries.Module.Domain.Repositories;
using RpgBooks.Modules.Identity.Domain.Entities;

/// <summary>
/// User domain repository.
/// </summary>
public interface IUserDomainRepository : IDomainRepository<User, int>
{
    /// <summary>
    /// Get user role by role name.
    /// </summary>
    /// <param name="roleName">Role name.</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns></returns>
    Task<Role?> GetUserRole(string roleName, CancellationToken cancellation = default);

    /// <summary>
    /// Check if user with given email exists.
    /// </summary>
    /// <param name="email">User email.</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>True if user exists, false otherwise.</returns>
    Task<bool> Exists(string email, CancellationToken cancellation = default);

    /// <summary>
    /// Get user by identifier.
    /// </summary>
    /// <param name="id">User identifier.</param>
    /// <param name="include">Function where any additional includes of navigation properties are added.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>User entity with the given email.</returns>
    Task<User?> GetByIdAsync(
        int id,
        Func<IQueryable<User>, IQueryable<User>>? include = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get user by email.
    /// </summary>
    /// <param name="email">User email.</param>
    /// <param name="include">Function where any additional includes of navigation properties are added.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>User entity with the given email.</returns>
    Task<User?> GetByEmailAsync(
        string email,
        Func<IQueryable<User>, IQueryable<User>>? include = null,
        CancellationToken cancellationToken = default);
}
