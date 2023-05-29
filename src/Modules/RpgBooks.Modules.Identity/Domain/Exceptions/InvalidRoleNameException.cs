namespace RpgBooks.Modules.Identity.Domain.Exceptions;

using RpgBooks.Libraries.Module.Domain.Exceptions;

internal sealed class InvalidRoleNameException : DomainValidationException
{
    public InvalidRoleNameException()
    {
    }

    public InvalidRoleNameException(string validationMessage)
        : base(validationMessage)
    {
    }
}
