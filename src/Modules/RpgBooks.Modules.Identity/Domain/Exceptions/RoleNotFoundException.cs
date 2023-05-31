namespace RpgBooks.Modules.Identity.Domain.Exceptions;

using RpgBooks.Libraries.Module.Domain.Exceptions;

/// <summary>
/// Exception raised when a role is not found.
/// </summary>
internal class RoleNotFoundException : DomainValidationException
{
    public RoleNotFoundException()
    {
    }

    public RoleNotFoundException(string validationMessage)
        : base(validationMessage)
    {
    }
}
