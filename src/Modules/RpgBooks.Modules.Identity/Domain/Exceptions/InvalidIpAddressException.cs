namespace RpgBooks.Modules.Identity.Domain.Exceptions;

using RpgBooks.Libraries.Module.Domain.Exceptions;

/// <summary>
/// Exception raised when an IP address is invalid.
/// </summary>
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
