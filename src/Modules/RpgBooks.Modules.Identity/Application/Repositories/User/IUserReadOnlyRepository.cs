namespace RpgBooks.Modules.Identity.Application.Repositories.User;

using RpgBooks.Libraries.Module.Application.Queries.Contracts;
using RpgBooks.Modules.Identity.Application.Repositories.User.Model;
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
    ValueTask<UserDetailsReadModel?> GetDetails(int id, CancellationToken cancellationToken = default!);

    /// <summary>
    /// Get actual security token for the given user.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="type">Security token type.</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns></returns>
    ValueTask<SecurityTokenReadModel?> GetActualToken(int userId, SecurityTokenType type, CancellationToken cancellation = default!);

    /// <summary>
    /// Get actual security token for the given user.
    /// </summary>
    /// <param name="userId">User identifier.</param>
    /// <param name="type">Security token type.</param>
    /// <param name="sessionId">Session id to which the token is assigned.</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns></returns>
    ValueTask<SecurityTokenReadModel?> GetActualToken(int userId, SecurityTokenType type, string? sessionId, CancellationToken cancellation = default!);
}
