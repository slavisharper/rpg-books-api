﻿namespace RpgBooks.Modules.Identity.Domain.Exceptions;
using RpgBooks.Libraries.Module.Domain.Exceptions;

internal sealed class InvalidUserException : DomainValidationException
{
    public InvalidUserException()
    {
    }

    public InvalidUserException(string validationMessage)
        : base(validationMessage)
    {
    }
}
