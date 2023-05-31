namespace RpgBooks.Modules.Identity.Domain.Validation;

using RpgBooks.Modules.Identity.Domain.Entities;
using RpgBooks.Modules.Identity.Domain.Exceptions;

using System;
using System.Runtime.CompilerServices;

/// <summary>
/// Helper class to validate <see cref="User"/> entities.
/// </summary>
internal static class UserValidation
{
    internal static class Values
    {
        public const int MinTitleLength = 2;

        public const int MaxTitleLength = 10;

        public const int MinNameLength = ValidationConstants.Person.MinNameLength;

        public const int MaxNameLength = ValidationConstants.Person.MaxNameLength;

        public const int MaxStampLength = 256;

        public const int MaxTokenLength = 512;

        public const int MaxPasswordHashLength = 512;
    }

    internal static class EnsureThat
    {
        internal static void HasValidTitle(string title, [CallerArgumentExpression(nameof(title))] string titleParamName = "")
        {
            Ensure.IsNotEmpty<InvalidUserTitleException>(title, titleParamName);
            Ensure.HasMinLength<InvalidUserTitleException>(title, Values.MinNameLength, titleParamName);
            Ensure.HasMaxLength<InvalidUserTitleException>(title, Values.MaxNameLength, titleParamName);
        }

        internal static void HasValidName(string name, [CallerArgumentExpression(nameof(name))] string nameParamName = "")
        {
            Ensure.IsNotEmpty<InvalidUserNameException>(name, nameParamName);
            Ensure.HasMinLength<InvalidUserNameException>(name, Values.MinNameLength, nameParamName);
            Ensure.HasMaxLength<InvalidUserNameException>(name, Values.MaxNameLength, nameParamName);
        }
    }
}
