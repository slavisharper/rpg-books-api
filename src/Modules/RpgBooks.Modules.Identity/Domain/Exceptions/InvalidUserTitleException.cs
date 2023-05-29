namespace RpgBooks.Modules.Identity.Domain.Exceptions;

using RpgBooks.Libraries.Module.Domain.Exceptions;

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
