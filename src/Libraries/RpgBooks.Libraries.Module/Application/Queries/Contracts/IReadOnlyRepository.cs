namespace RpgBooks.Libraries.Module.Application.Queries.Contracts;

using RpgBooks.Libraries.Module.Domain.Entities.Abstractions;

/// <summary>
/// Repository for read-only db operations
/// </summary>
/// <typeparam name="TEntity">Type of the db entity.</typeparam>
/// <typeparam name="TKey">Type of the db entity key.</typeparam>
public interface IReadOnlyRepository<TEntity, TKey>
    where TEntity : IEntity<TKey>
{
    /// <summary>
    /// Gets all entities as queryable.
    /// </summary>
    /// <returns>Queried entities.</returns>
    IQueryable<TEntity> All();
}
