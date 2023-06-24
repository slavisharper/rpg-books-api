namespace RpgBooks.Modules.Catalog.Domain.Entities;

using RpgBooks.Libraries.Module.Domain.Entities;

/// <summary>
/// Product category that groups similar products.
/// </summary>
public sealed class ProductCategory : BaseEntity<int>
{
    internal ProductCategory(string name, string? description)
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
