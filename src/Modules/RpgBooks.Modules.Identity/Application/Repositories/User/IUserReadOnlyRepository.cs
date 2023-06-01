namespace RpgBooks.Modules.Identity.Application.Repositories.User;

using RpgBooks.Libraries.Module.Application.Queries.Contracts;
using RpgBooks.Modules.Identity.Domain.Entities;

/// <summary>
/// User read only repository. Used for querying users.
/// </summary>
internal interface IUserReadOnlyRepository : IReadOnlyRepository
{
    /// <summary>
    /// Gets the user details read-only model.
    /// </summary>
    /// <param name="id">User identifier.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>User details read-only model.</returns>
    Task<UserDetailsReadModel?> GetDetails(int id, CancellationToken cancellationToken = default!);
}
