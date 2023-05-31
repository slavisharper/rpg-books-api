namespace RpgBooks.Modules.Identity.Domain.Exceptions;

using RpgBooks.Libraries.Module.Domain.Exceptions;

/// <summary>
/// Exception raised when a role name is invalid.
/// </summary>
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
