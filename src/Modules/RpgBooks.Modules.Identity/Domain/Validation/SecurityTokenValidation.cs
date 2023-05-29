﻿namespace RpgBooks.Modules.Identity.Domain.Validation;

using RpgBooks.Modules.Identity.Domain.Exceptions;

using System;
using System.Runtime.CompilerServices;

internal static class SecurityTokenValidation
{
    internal static class Values
    {
        public const int MaxTokenLenght = 512;
    }

    internal static class EnsureThat
    {
        internal static void HasValidToken(string? token, [CallerArgumentExpression(nameof(token))] string tokenParamName = "")
        {
            Ensure.HasMaxLength<InvalidSecurityTokenException>(token, Values.MaxTokenLenght, tokenParamName);
        }
    }   
}
