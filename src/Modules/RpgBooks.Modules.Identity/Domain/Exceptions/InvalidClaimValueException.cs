namespace RpgBooks.Modules.Identity.Domain.Exceptions;

using RpgBooks.Libraries.Module.Domain.Exceptions;

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
