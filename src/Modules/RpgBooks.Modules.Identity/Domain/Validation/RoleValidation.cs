namespace RpgBooks.Modules.Identity.Domain.Validation;

using RpgBooks.Modules.Identity.Domain.Entities;
using RpgBooks.Modules.Identity.Domain.Exceptions;

using System.Runtime.CompilerServices;

/// <summary>
/// Helper class to validate <see cref="Role"/> properties.
/// </summary>
internal static class RoleValidation
{
    internal static class Values
    {
        public const int MaxNameLenght = 50;
    }
    internal static class EnsureThat
    {
        internal static void HasValidName(string? name, [CallerArgumentExpression(nameof(name))] string nameParamName = "")
        {
            Ensure.HasMaxLength<InvalidRoleNameException>(name, Values.MaxNameLenght, nameParamName);
        }
    }
}
