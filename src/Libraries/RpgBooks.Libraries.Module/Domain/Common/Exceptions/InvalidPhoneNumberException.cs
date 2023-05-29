namespace RpgBooks.Libraries.Module.Domain.Common.Exceptions;

using RpgBooks.Libraries.Module.Domain.Exceptions;

/// <summary>
/// Invalid phone number exception.
/// </summary>
public sealed class InvalidPhoneNumberException : DomainValidationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidPhoneNumberException"/> class.
    /// </summary>
    public InvalidPhoneNumberException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidPhoneNumberException"/> class.
    /// </summary>
    /// <param name="validationMessage">Validation message.</param>
    public InvalidPhoneNumberException(string validationMessage)
        : base(validationMessage)
    {
    }
}
