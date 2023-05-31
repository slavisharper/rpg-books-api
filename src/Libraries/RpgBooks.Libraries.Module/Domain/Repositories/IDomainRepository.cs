namespace RpgBooks.Libraries.Module.Domain.Repositories;

using RpgBooks.Libraries.Module.Domain.Entities.Abstractions;

/// <summary>
/// Base domain repository.
/// </summary>
/// <typeparam name="TEntity">Type of the aggregate root entity.</typeparam>
/// <typeparam name="TKey">Type of the identifier of the aggregate root entity.</typeparam>
public interface IDomainRepository<TEntity, TKey>
        where TEntity : IEntity<TKey>, IAggregateRoot
{
    /// <summary>
    /// Adds entity of <typeparamref name="TEntity"/> to the given domain repository.
    /// </summary>
    /// <param name="entity">Entity to add.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Task that represents add operation.</returns>
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the entity of <typeparamref name="TEntity"/> with the provided key of type <typeparamref name="TKey"/>.
    /// </summary>
    /// <param name="key">Id of the searched entity.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The matching entity.</returns>
    Task<TEntity?> GetAsync(TKey key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Save changes to the repository.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Task that represents save operation.</returns>
    Task SaveAsync(CancellationToken cancellationToken = default);
}
