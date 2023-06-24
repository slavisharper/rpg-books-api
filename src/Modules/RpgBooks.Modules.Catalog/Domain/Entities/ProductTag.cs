namespace RpgBooks.Modules.Catalog.Domain.Entities;

using RpgBooks.Libraries.Module.Domain.Entities;

/// <summary>
/// Keeps tags information that can be associated with a product.
/// This is alternative to categories that can be used to group products in a different way.
/// </summary>
public sealed class ProductTag : BaseEntity<int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProductTag"/> class.
    /// </summary>
    /// <param name="name">Name of the product tag.</param>
    internal ProductTag(string name)
	{
		this.Name = name;
	}

    /// <summary>
    /// Gets the name of the product tag.
    /// </summary>
    public string Name { get; private set; }
}
