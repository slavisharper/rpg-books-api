namespace RpgBooks.Libraries.Module.Infrastructure.Persistence.Repositories;

using RpgBooks.Libraries.Module.Application.Queries.Contracts;
using RpgBooks.Libraries.Module.Domain.Entities.Abstractions;

using System.Data.Common;
using System.Linq;

/// <summary>
/// Base read only repository implementation.
/// </summary>
/// <typeparam name="TEntity">Type of the db entity.</typeparam>
/// <typeparam name="TKey">Type of the db entity key.</typeparam>
public abstract class BaseReadOnlyRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey>
    where TEntity : IEntity<TKey>
{
    /// <summary>
    /// Dapper db connection.
    /// </summary>
    protected readonly DbConnection dbConnection;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseReadOnlyRepository{TEntity, TKey}"/> class.
    /// </summary>
    /// <param name="dbConnection"></param>
    public BaseReadOnlyRepository(DbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    /// <inheritdoc />
    public abstract IQueryable<TEntity> All();
}
