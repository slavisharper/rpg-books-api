namespace RpgBooks.Libraries.Module.Domain.Abstractions;

using RpgBooks.Libraries.Module.Domain.Entities.Abstractions;

/// <summary>
/// I domain entity builder contract.
/// </summary>
/// <typeparam name="T">Type of the built entity.</typeparam>
public interface IBuilder<T>
    where T : IBaseEntity, IAggregateRoot
{
    /// <summary>
    /// Builds the entity.
    /// </summary>
    /// <returns>Built entity.</returns>
    T Build();
}

/// <see cref="IBuilder{T}"/>
public interface IAsyncBuilder<T>
    where T : IBaseEntity, IAggregateRoot
{
    /// <summary>
    /// Builds the entity asynchronously.
    /// </summary>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>Built entity.</returns>
    Task<T> Build(CancellationToken cancellation = default);
}
