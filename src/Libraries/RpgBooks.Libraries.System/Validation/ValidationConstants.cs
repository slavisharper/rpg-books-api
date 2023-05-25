namespace System;

using System.Text.RegularExpressions;

public static partial class ValidationConstants
{
    public const int Zero = 0;
    public const int MaxGuidLength = 38;

    public static class Web
    {
        public const int MaxUrlLength = 2048;
    }

    public static partial class Phone
    {
        public const int MinLength = 5;
        public const int MaxLength = 16;
        public static readonly Regex Format = PhoneFormatRegex();

        [GeneratedRegex("^([+]?[\\s0-9]+)?(\\d{3}|[(]?[0-9]+[)])?([-]?[\\s]?[0-9])+$", RegexOptions.Compiled)]
        private static partial Regex PhoneFormatRegex();
    }

    public static partial class Email
    {
        public const int MinLength = 3;
        public const int MaxLength = 250;
        public static readonly Regex Format = EmailFormatRegex();

        [GeneratedRegex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", RegexOptions.Compiled)]
        private static partial Regex EmailFormatRegex();
    }

    public static class Human
    {
        public const int MinNameLength = 2;

        public const int MaxNameLength = 50;

        // Currently the oldest person is 118 years old. And oldest verified record age is 122 years old.
        public const int MaxAge = 125;
    }
}
