namespace RpgBooks.Modules.Identity.Domain.Validation;

using RpgBooks.Modules.Identity.Domain.Exceptions;

using System.Runtime.CompilerServices;

internal static class LoginRecordValidation
{
    internal static class Values
    {
        public const int MaxIpAddressLenght = 50;

        public const int MaxUserAgentLenght = 4096;
    }

    internal static class EnsureThat
    {
        internal static void HasValidIpAddress(string? ipAddress, [CallerArgumentExpression(nameof(ipAddress))] string ipAddressParamName = "")
        {
            Ensure.HasMaxLength<InvalidIpAddressException>(ipAddress, Values.MaxIpAddressLenght, ipAddressParamName);
        }
        internal static void HasValidUserAgent(string? userAgent, [CallerArgumentExpression(nameof(userAgent))] string userAgentParamName = "")
        {
            Ensure.HasMaxLength<InvalidUserAgentException>(userAgent, Values.MaxUserAgentLenght, userAgentParamName);
        }
    }
}
