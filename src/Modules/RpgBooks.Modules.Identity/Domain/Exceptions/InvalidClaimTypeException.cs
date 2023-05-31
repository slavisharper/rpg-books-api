namespace RpgBooks.Modules.Identity.Domain.Exceptions;

using RpgBooks.Libraries.Module.Domain.Exceptions;

/// <summary>
/// Exception raised when a claim type is invalid.
/// </summary>
internal sealed class InvalidClaimTypeException : DomainValidationException
{
    public InvalidClaimTypeException()
    {
    }

    public InvalidClaimTypeException(string validationMessage)
        : base(validationMessage)
    {
    }
}
