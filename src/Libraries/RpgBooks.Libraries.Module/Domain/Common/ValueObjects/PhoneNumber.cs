namespace RpgBooks.Libraries.Module.Domain.Common.ValueObjects;

using RpgBooks.Libraries.Module.Domain.Common.Exceptions;
using RpgBooks.Libraries.Module.Domain.Entities;

/// <summary>
/// Value object that holds phone number data.
/// </summary>
public sealed record PhoneNumber : ValueObject<PhoneNumber>
{
    /// <summary>
    /// Creates new Phone number instance.
    /// </summary>
    /// <param name="value">Phone number value.</param>
    public PhoneNumber(string value)
    {
        Validate(value);
        Value = value;
    }

    /// <summary>
    /// Gets phone number value.
    /// </summary>
    public string Value { get; init; }

    /// <summary>
    /// Implicitly converts phone number to string.
    /// </summary>
    /// <param name="number">Phone number value.</param>
    public static implicit operator string(PhoneNumber number) => number.Value;

    /// <summary>
    /// Implicitly converts string to phone number.
    /// </summary>
    /// <param name="number">Phone number value.</param>
    public static implicit operator PhoneNumber(string number) => new(number);

    private static void Validate(string phoneNumber)
        => Ensure.IsValidPhoneNumber<InvalidPhoneNumberException>(phoneNumber);

    /// <inheritdoc/>
    public override PhoneNumber Copy()
        => new PhoneNumber(this);
}
