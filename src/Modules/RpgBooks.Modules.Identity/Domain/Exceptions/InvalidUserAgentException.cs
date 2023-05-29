namespace RpgBooks.Modules.Identity.Domain.Exceptions;

using RpgBooks.Libraries.Module.Domain.Exceptions;

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
