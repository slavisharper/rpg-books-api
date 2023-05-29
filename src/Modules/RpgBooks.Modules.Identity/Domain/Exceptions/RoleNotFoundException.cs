namespace RpgBooks.Modules.Identity.Domain.Exceptions;

using RpgBooks.Libraries.Module.Domain.Exceptions;

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
