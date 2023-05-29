namespace RpgBooks.Libraries.Module.Infrastructure.Persistence.Abstractions;

/// <summary>
/// Database initializer.
/// </summary>
public interface IDbInitializer
{
    /// <summary>
    /// Initializes the database. This method should be called only once at application start.
    /// Migrates the database and seeds initial data if needed.
    /// </summary>
    void Initialize();

    /// <seealso cref="Initialize"/>"/>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task InitializeAsync(CancellationToken cancellationToken = default);
}
