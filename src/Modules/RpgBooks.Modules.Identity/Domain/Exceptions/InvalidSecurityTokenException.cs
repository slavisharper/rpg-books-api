namespace RpgBooks.Modules.Identity.Domain.Exceptions;

using RpgBooks.Libraries.Module.Domain.Exceptions;

/// <summary>
/// Exception raised when a security token is invalid.
/// </summary>
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
