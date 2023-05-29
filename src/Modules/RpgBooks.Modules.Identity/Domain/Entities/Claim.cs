namespace RpgBooks.Modules.Identity.Domain.Entities;

using RpgBooks.Modules.Identity.Domain.Validation;
using RpgBooks.Libraries.Module.Domain.Entities;

/// <summary>
/// Claim entity.
/// </summary>
public sealed class Claim : BaseEntity<int>
{
    internal Claim(string type, string value, string valueType)
    {
        this.Type = type;
        this.Value = value;
        this.ValueType = valueType;

        this.Validate();
    }

    /// <summary>
    /// Gets the claim type.
    /// </summary>
    public string Type { get; private set; }

    /// <summary>
    /// Gets the claim value.
    /// </summary>
    public string Value { get; private set; }

    /// <summary>
    /// Gets the claim value type.
    /// </summary>
    public string ValueType { get; private set; }

    private void Validate()
    {
        ClaimValidation.EnsureThat.HasValidType(this.Type);
        ClaimValidation.EnsureThat.HasalidValue(this.Value);
        ClaimValidation.EnsureThat.HasValidValueType(this.ValueType);
    }
}
