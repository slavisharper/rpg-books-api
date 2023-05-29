namespace RpgBooks.Modules.Identity.Domain.Exceptions;

using RpgBooks.Libraries.Module.Domain.Exceptions;

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
