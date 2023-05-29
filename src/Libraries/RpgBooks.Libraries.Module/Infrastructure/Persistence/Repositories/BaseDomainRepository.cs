namespace RpgBooks.Libraries.Module.Infrastructure.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;

using RpgBooks.Libraries.Module.Domain.Entities.Abstractions;
using RpgBooks.Libraries.Module.Domain.Repositories;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Base domain repository.
/// </summary>
/// <typeparam name="TContext">Type of the db context.</typeparam>
/// <typeparam name="TEntity">Type of the db entity.</typeparam>
/// <typeparam name="TKey">Type of the db entity key.</typeparam>
public abstract class BaseDomainRepository<TContext, TEntity, TKey> : IDomainRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>, IAggregateRoot
    where TContext : DbContext
{
    /// <summary>
    /// Db context instance.
    /// </summary>
    protected readonly TContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseDomainRepository{TContext, TEntity, TKey}"/> class.
    /// </summary>
    /// <param name="context">Db Context.</param>
    public BaseDomainRepository(TContext context)
    {
        this.context = context;
    }

    /// <inheritdoc/>
    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        => await this.context.AddAsync(entity, cancellationToken);

    /// <inheritdoc/>
    public async Task<TEntity?> GetAsync(TKey key, CancellationToken cancellationToken = default)
        => await this.context.FindAsync<TEntity>(key);

    /// <inheritdoc/>
    public async Task SaveAsync(CancellationToken cancellationToken = default)
        => await this.context.SaveChangesAsync(cancellationToken);
}
