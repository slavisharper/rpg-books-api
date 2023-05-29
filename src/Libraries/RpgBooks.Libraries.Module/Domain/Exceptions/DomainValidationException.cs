namespace RpgBooks.Libraries.Module.Domain.Exceptions;

using System;

/// <summary>
/// Raised when domain entity validation fails.
/// </summary>
public abstract class DomainValidationException : DomainException, IValidationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainValidationException"/> class.
    /// </summary>
    protected DomainValidationException()
    {
        this.ValidationMessage = default!;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DomainValidationException"/> class.
    /// </summary>
    /// <param name="validationMessage">Validation message.</param>
    protected DomainValidationException(string validationMessage)
        : base(validationMessage)
    {
        this.ValidationMessage = validationMessage;
    }

    /// <inheritdoc/>
    public string ValidationMessage { get; set; }
}
