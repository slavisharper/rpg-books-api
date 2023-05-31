namespace RpgBooks.Modules.Identity.Domain.Exceptions;

using RpgBooks.Libraries.Module.Domain.Exceptions;

/// <summary>
/// Exception raised when a user agent is invalid.
/// </summary>
internal sealed class InvalidUserAgentException : DomainValidationException
{
    public InvalidUserAgentException()
    {
    }

    public InvalidUserAgentException(string validationMessage)
        : base(validationMessage)
    {
    }
}
