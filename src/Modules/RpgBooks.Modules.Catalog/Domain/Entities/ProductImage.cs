namespace RpgBooks.Modules.Catalog.Domain.Entities;

using RpgBooks.Libraries.Module.Domain.Common.ValueObjects;
using RpgBooks.Libraries.Module.Domain.Entities;

/// <summary>
/// Represents a product image domain entity
/// </summary>
public sealed class ProductImage : BaseEntity<int>
{
    internal ProductImage(LocalFile location)
    {
        this.Location = location;
    }

    private ProductImage()
    {
        this.Location = default!;
    }

    /// <summary>
    /// Gets the location of the image file.
    /// </summary>
    public LocalFile Location { get; private set; }
}
