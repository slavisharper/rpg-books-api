namespace RpgBooks.Modules.Identity.Domain.Exceptions;
using RpgBooks.Libraries.Module.Domain.Exceptions;

/// <summary>
/// Exception raised when a user is invalid.
/// </summary>
internal sealed class InvalidUserException : DomainValidationException
{
    public InvalidUserException()
    {
    }

    public InvalidUserException(string validationMessage)
        : base(validationMessage)
    {
    }
}
