namespace RpgBooks.Modules.Catalog.Domain.Enumerations;

using RpgBooks.Libraries.Module.Domain.Entities;

/// <summary>
/// Represents a product specification value type enumeration.
/// </summary>
public sealed class ProductSpecificationType : Enumeration
{
    private ProductSpecificationType(int id, string name) : base(id, name)
    {
    }

    /// <summary>
    /// Represents a text value type
    /// </summary>
    public static readonly ProductSpecificationType Text = new(1, nameof(Text));

    /// <summary>
    /// Represents a number value type
    /// </summary>
    public static readonly ProductSpecificationType Number = new(2, nameof(Number));

    /// <summary>
    /// Represents a boolean value type
    /// </summary>
    public static readonly ProductSpecificationType Boolean = new(3, nameof(Boolean));

    /// <summary>
    /// Represents a date value type
    /// </summary>
    public static readonly ProductSpecificationType Date = new(4, nameof(Date));

    /// <summary>
    /// Represents a date + time value type
    /// </summary>
    public static readonly ProductSpecificationType DateTime = new(6, nameof(DateTime));
}
