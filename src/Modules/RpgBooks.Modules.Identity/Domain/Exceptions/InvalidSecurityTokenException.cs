namespace RpgBooks.Modules.Identity.Domain.Exceptions;

using RpgBooks.Libraries.Module.Domain.Exceptions;

internal sealed class InvalidSecurityTokenException : DomainValidationException
{
    public InvalidSecurityTokenException()
    {
    }

    public InvalidSecurityTokenException(string validationMessage)
        : base(validationMessage)
    {
    }
}
