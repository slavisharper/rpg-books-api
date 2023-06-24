namespace RpgBooks.Modules.Catalog.Domain.Entities;

using RpgBooks.Libraries.Module.Domain.Entities;

/// <summary>
/// Product type that indicates the various types of products.
/// </summary>
public sealed class ProductType : BaseEntity<int>
{
    internal ProductType(string name, string? description)
    {
        this.Name = name;
        this.Description = description;
    }

    /// <summary>
    /// Gets the product type name.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the product type description.
    /// </summary>
    public string? Description { get; private set; }
}
