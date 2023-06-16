namespace RpgBooks.Modules.Catalog.Domain.Entities;

using RpgBooks.Libraries.Module.Domain.Entities;
using RpgBooks.Libraries.Module.Domain.Entities.Abstractions;

/// <summary>
/// Represents a product domain entity
/// </summary>
public sealed class Product : BaseEntity<int>, IAggregateRoot, IConcurrentEntity
{
    /// <inheritdoc/>
    public byte[] ConcurrencyStamp { get; private set; }
}
