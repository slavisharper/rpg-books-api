namespace RpgBooks.Modules.Identity.Domain.Exceptions;

using RpgBooks.Libraries.Module.Domain.Exceptions;

/// <summary>
/// Exception raised when a claim value is invalid.
/// </summary>
internal sealed class InvalidClaimValueException : DomainValidationException
{
    public InvalidClaimValueException()
    {
    }

    public InvalidClaimValueException(string validationMessage)
        : base(validationMessage)
    {
    }
}
