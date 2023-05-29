namespace RpgBooks.Libraries.Module.Domain.Entities.Abstractions;

/// <summary>
/// Entity with row version for concurrency checks.
/// </summary>
public interface IConcurrentEntity
{
    /// <summary>
    /// Gets the row version.
    /// </summary>
    byte[] ConcurrencyStamp { get; }
}
