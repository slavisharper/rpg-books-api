namespace RpgBooks.Modules.Catalog.Domain.Entities;

using RpgBooks.Libraries.Module.Domain.Common.ValueObjects;
using RpgBooks.Libraries.Module.Domain.Entities;
using RpgBooks.Libraries.Module.Domain.Entities.Abstractions;

/// <summary>
/// Represents a product domain entity
/// </summary>
public sealed class Product : BaseEntity<int>, IAggregateRoot, IConcurrentEntity
{
    private readonly ICollection<ProductCategory> categories;
    private readonly ICollection<ProductImage> images;
    private readonly ICollection<ProductSpecification> specifications;
    private readonly ICollection<ProductTag> tags;
    private readonly ICollection<Product> innerProducts;

    private Product(
        string name,
        string code,
        string? shortDescription,
        string description,
        string? publisher,
        string? author,
        int? validityPeriodInDays,
        byte[] concurrencyStamp)
    {
        this.Name = name;
        this.Code = code;
        this.ShortDescription = shortDescription;
        this.Description = description;
        this.Publisher = publisher;
        this.Author = author;
        this.ValidityPeriodInDays = validityPeriodInDays;
        this.ConcurrencyStamp = concurrencyStamp;

        this.OwnershipType = default!;
        this.MainImage = default!;

        this.categories = new HashSet<ProductCategory>();
        this.images = new HashSet<ProductImage>();
        this.specifications = new HashSet<ProductSpecification>();
        this.tags = new HashSet<ProductTag>();
        this.innerProducts = new HashSet<Product>();
    }

    internal Product(string name, string code, LocalFile mainImage, ProductOwnershipType ownershipType)
    {
        this.Name = name;
        this.Code = code;
        this.MainImage = mainImage;
        this.OwnershipType = ownershipType;

        this.ConcurrencyStamp = Array.Empty<byte>();
        this.categories = new HashSet<ProductCategory>();
        this.images = new HashSet<ProductImage>();
        this.specifications = new HashSet<ProductSpecification>();
        this.tags = new HashSet<ProductTag>();
        this.innerProducts = new HashSet<Product>();
    }

    internal Product(string name, string code, LocalFile mainImage, string author, string publisher, ProductOwnershipType ownershipType)
        : this(name, code, mainImage, ownershipType)
    {
        this.Author = author;
        this.Publisher = publisher;
    }
    
    /// <summary>
    /// Gets the product name.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the unique product code.
    /// </summary>
    public string Code { get; private set;}

    /// <summary>
    /// Gets the product short description. Used in the product list.
    /// </summary>
    public string? ShortDescription { get; private set; }

    /// <summary>
    /// Gets the product description.
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    /// Gets the product main image. Used in the product list.
    /// </summary>
    public LocalFile MainImage { get; private set; }

    /// <summary>
    /// Gets the publisher of the product.
    /// </summary>
    public string? Publisher { get; private set; }

    /// <summary>
    /// Gets the author of the product.
    /// </summary>
    public string? Author { get; private set; }

    /// <summary>
    /// Gets the product validity period in days.
    /// <para>This is valid for rents and subscriptions.</para>
    /// </summary>
    public int? ValidityPeriodInDays { get; private set; }

    /// <inheritdoc/>
    public byte[] ConcurrencyStamp { get; private set; }

    /// <summary>
    /// Gets the ownership type that indicates the product purchase lifetime.
    /// </summary>
    public ProductOwnershipType OwnershipType { get; private set; }

    /// <summary>
    /// Gets product categories that are associated with this product.
    /// </summary>
    public IReadOnlyCollection<ProductCategory> Categories => this.categories.ToArray();

    /// <summary>
    /// Gets the images that are associated with this product. This is simplified product gallery.
    /// </summary>
    public IReadOnlyCollection<ProductImage> Images => this.images.ToArray();

    /// <summary>
    /// Gets the specifications that are associated with this product.
    /// </summary>
    public IReadOnlyCollection<ProductSpecification> Specifications => this.specifications.ToArray();

    /// <summary>
    /// Gets the tags that are associated with this product.
    /// This is alternative to categories for additional grouping of products.
    /// </summary>
    public IReadOnlyCollection<ProductTag> Tags => this.tags.ToArray();

    /// <summary>
    /// Gets the products that are included in this bundle product.
    /// </summary>
    public IReadOnlyCollection<Product> InnerProducts => this.innerProducts.ToArray();
}
