namespace RpgBooks.Modules.Identity.Domain.Exceptions;

using RpgBooks.Libraries.Module.Domain.Exceptions;

/// <summary>
/// Exception raised when a claim value type is invalid.
/// </summary>
internal sealed class InvalidClaimValueTypeException : DomainValidationException
{
    public InvalidClaimValueTypeException()
    {
    }

    public InvalidClaimValueTypeException(string validationMessage)
        : base(validationMessage)
    {
    }
}
