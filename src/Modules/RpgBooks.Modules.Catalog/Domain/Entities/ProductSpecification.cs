namespace RpgBooks.Modules.Catalog.Domain.Entities;

using RpgBooks.Libraries.Module.Domain.Entities;

public sealed class ProductSpecification : BaseEntity<int>
{
    internal ProductSpecification(string name, string? value)
    {
        this.Name = name;
        this.Value = value;
        this.ValueType = ProductSpecificationType.Text;
    }

    internal ProductSpecification(string name, string? value, string? valueUnit, ProductSpecificationType valueType)
        : this(name, value)
    {
        this.ValueUnit = valueUnit;
        this.ValueType = valueType;
    }

    private ProductSpecification(string name, string? value, string? valueUnit)
    {
        this.Name = name;
        this.Value = value;
        this.ValueUnit = valueUnit;

        this.ValueType = default!;
    }

    public string Name { get; private set; }

    public string? Value { get; private set; }

    public string? ValueUnit { get; private set; }

    public ProductSpecificationType ValueType { get; private set; }
}
