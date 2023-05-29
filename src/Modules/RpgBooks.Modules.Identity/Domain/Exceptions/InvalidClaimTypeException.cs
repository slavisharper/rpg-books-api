namespace RpgBooks.Modules.Identity.Domain.Exceptions;

using RpgBooks.Libraries.Module.Domain.Exceptions;

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
