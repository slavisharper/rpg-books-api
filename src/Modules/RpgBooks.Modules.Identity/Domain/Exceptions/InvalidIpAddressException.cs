namespace RpgBooks.Modules.Identity.Domain.Exceptions;

using RpgBooks.Libraries.Module.Domain.Exceptions;

internal sealed class InvalidIpAddressException : DomainValidationException
{
    public InvalidIpAddressException()
    {
    }

    public InvalidIpAddressException(string validationMessage)
        : base(validationMessage)
    {
    }
}
