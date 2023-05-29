namespace RpgBooks.Modules.Identity.Domain.Exceptions;

using RpgBooks.Libraries.Module.Domain.Exceptions;

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
