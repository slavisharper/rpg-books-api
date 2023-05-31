namespace RpgBooks.Libraries.Module.Domain.Common.ValueObjects;

using RpgBooks.Libraries.Module.Domain.Common.Exceptions;
using RpgBooks.Libraries.Module.Domain.Entities;

/// <summary>
/// Value object that holds email data.
/// </summary>
public sealed record Email : ValueObject<Email>
{
    /// <summary>
    /// Creates new Email instance.
    /// </summary>
    /// <param name="value">Email value.</param>
    public Email(string value)
    {
        Validate(value);
        Value = value;
    }

    /// <summary>
    /// Gets email value.
    /// </summary>
    public string Value { get; init; }

    /// <summary>
    /// Gets the user name part of the email.
    /// </summary>
    public string UserName => Value.Split('@')[0];

    /// <summary>
    /// Gets the domain part of the email.
    /// </summary>
    public string Domain => Value.Split('@')[1];

    /// <summary>
    /// Implicitly converts email to string.
    /// </summary>
    /// <param name="emal">Email value.</param>
    public static implicit operator string(Email emal) => emal.Value;

    /// <summary>
    /// Implicitly converts string to email.
    /// </summary>
    /// <param name="email">Email value.</param>
    public static implicit operator Email(string email) => new(email);

    private static void Validate(string email)
        => Ensure.IsValidEmail<InvalidPhoneNumberException>(email);

    /// <inheritdoc/>
    public override Email Copy()
        => new Email(this);
}