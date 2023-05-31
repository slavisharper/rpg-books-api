namespace RpgBooks.Libraries.Module.Domain.Entities.Abstractions;

/// <summary>
/// Entity base.
/// </summary>
/// <typeparam name="T">Type of Id.</typeparam>
public interface IEntity<T> : IBaseEntity
{
    /// <summary>
    /// Gets Id field.
    /// </summary>
    T Id { get; }
}
