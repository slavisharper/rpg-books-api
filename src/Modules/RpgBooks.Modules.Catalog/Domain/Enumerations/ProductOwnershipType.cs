namespace RpgBooks.Modules.Catalog.Domain.Enumerations;

/// <summary>
/// Product ownership type that indicates the various ownership options for the product
/// </summary>
public sealed class ProductOwnershipType : Enumeration
{
    private ProductOwnershipType(int value)
        : base(value, FromValue<ProductOwnershipType>(value).Name)
    {
    }

    private ProductOwnershipType(int value, string name)
        : base(value, name)
    {
    }

    /// <summary>
    /// Gets the product ownership type responsible for a normal product purchase.
    /// </summary>
    public static readonly ProductOwnershipType OneTimePurchase = new(1, nameof(OneTimePurchase));

    /// <summary>
    /// Gets the product ownership type responsible for renting the given product for given amount of time.
    /// <para>This is subscription with one time payment.</para>
    /// </summary>
    public static readonly ProductOwnershipType Rent = new(2, nameof(Rent));

    /// <summary>
    /// Gets the product ownership type responsible for buying multiple products and own them forever.
    /// <para>This is one time purchase of many bundled products at once.</para>
    ///</summary>
    public static readonly ProductOwnershipType Bundle = new(3, nameof(Bundle));

    /// <summary>
    /// Gets the product ownership type responsible for owning subscription for a product.
    /// <para>This is a rent of bundled products with recurring payment over given period of time.</para>
    ///</summary>
    public static readonly ProductOwnershipType Subscription = new(4, nameof(Subscription));
}
