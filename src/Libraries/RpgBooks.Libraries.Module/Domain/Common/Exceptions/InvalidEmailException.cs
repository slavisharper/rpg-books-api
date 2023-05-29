namespace RpgBooks.Libraries.Module.Domain.Common.Exceptions;

using RpgBooks.Libraries.Module.Domain.Exceptions;

/// <summary>
/// Represents error that occurs when email is invalid.
/// </summary>
public sealed class InvalidEmailException : DomainValidationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidEmailException"/> class.
    /// </summary>
    public InvalidEmailException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidEmailException"/> class.
    /// </summary>
    /// <param name="validationMessage">Validation message.</param>
    public InvalidEmailException(string validationMessage)
        : base(validationMessage)
    {
    }
}
