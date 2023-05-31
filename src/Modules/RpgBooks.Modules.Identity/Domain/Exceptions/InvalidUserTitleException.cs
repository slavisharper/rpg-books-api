namespace RpgBooks.Modules.Identity.Domain.Exceptions;

using RpgBooks.Libraries.Module.Domain.Exceptions;

/// <summary>
/// Exception raised when a user title is invalid.
/// </summary>
internal sealed class InvalidUserTitleException : DomainValidationException
{
    public InvalidUserTitleException()
    {
    }

    public InvalidUserTitleException(string validationMessage)
        : base(validationMessage)
    {
    }
}
