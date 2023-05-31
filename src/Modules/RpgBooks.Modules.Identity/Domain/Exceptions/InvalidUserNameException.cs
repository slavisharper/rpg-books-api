namespace RpgBooks.Modules.Identity.Domain.Exceptions;

using RpgBooks.Libraries.Module.Domain.Exceptions;

/// <summary>
/// Exception raised when a user name is invalid.
/// </summary>
internal sealed class InvalidUserNameException : DomainValidationException
{
    public InvalidUserNameException()
    {
    }

    public InvalidUserNameException(string validationMessage)
        : base(validationMessage)
    {
    }
}
