namespace RpgBooks.Libraries.Module.Infrastructure.Persistence.Abstractions;

using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

using RpgBooks.Libraries.Module.Application.Services.CurrentUser;

/// <summary>
/// Base database context interface.
/// </summary>
public interface IDbContext
{
    /// <summary>
    /// Gets the current user service if available.
    /// </summary>
    ICurrentUserService? CurrentUserService { get; }

    /// <summary>
    /// Gets db context change tracked.
    /// </summary>
    ChangeTracker ChangeTracker { get; }

    /// <summary>
    /// Gets the database instance.
    /// </summary>
    DatabaseFacade Database { get; }
}
