namespace RpgBooks.Modules.Identity.Domain.Validation;

using RpgBooks.Modules.Identity.Domain.Entities;
using RpgBooks.Modules.Identity.Domain.Exceptions;

using System;
using System.Runtime.CompilerServices;

/// <summary>
/// Helper class to validate <see cref="SecurityToken"/> entities.
/// </summary>
internal static class SecurityTokenValidation
{
    internal static class Values
    {
        public const int MaxTokenLenght = 512;

        public const int MaxSessionIdLenght = 256;
    }

    internal static class EnsureThat
    {
        internal static void HasValidToken(string? token, [CallerArgumentExpression(nameof(token))] string tokenParamName = "")
        {
            Ensure.IsNotNull<string, InvalidSecurityTokenException>(token, tokenParamName);
            Ensure.HasMaxLength<InvalidSecurityTokenException>(token, Values.MaxTokenLenght, tokenParamName);
        }

        internal static void HasValidSessionId(string? sessionId, [CallerArgumentExpression(nameof(sessionId))] string sessionIdParamName = "")
        {
            Ensure.HasMaxLength<InvalidSecurityTokenException>(sessionId, Values.MaxSessionIdLenght, sessionIdParamName);
        }
    }   
}
