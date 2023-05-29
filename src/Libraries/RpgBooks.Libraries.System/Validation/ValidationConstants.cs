namespace System;

using System.Text.RegularExpressions;

/// <summary>
/// Common validation constants.
/// </summary>
public static partial class ValidationConstants
{
    /// <summary>
    /// Zero value.
    /// </summary>
    public const int Zero = 0;

    /// <summary>
    /// Max aloud GUID length.
    /// </summary>
    public const int MaxGuidLength = 38;

    /// <summary>
    /// Web constants.
    /// </summary>
    public static class Web
    {
        /// <summary>
        /// Max URL length.
        /// </summary>
        public const int MaxUrlLength = 2048;
    }

    /// <summary>
    /// Phone related validation constants.
    /// </summary>
    public static partial class Phone
    {
        /// <summary>
        /// Min phone length.
        /// </summary>
        public const int MinLength = 5;

        /// <summary>
        /// Max phone length.
        /// </summary>
        public const int MaxLength = 16;

        /// <summary>
        /// Phone format regex.
        /// </summary>
        public static readonly Regex Format = PhoneFormatRegex();

        [GeneratedRegex("^([+]?[\\s0-9]+)?(\\d{3}|[(]?[0-9]+[)])?([-]?[\\s]?[0-9])+$", RegexOptions.Compiled)]
        private static partial Regex PhoneFormatRegex();
    }

    /// <summary>
    /// Email validation constants
    /// </summary>
    public static partial class Email
    {
        /// <summary>
        /// Min email length.
        /// </summary>
        public const int MinLength = 3;

        /// <summary>
        /// Max allowed email length.
        /// </summary>
        public const int MaxLength = 250;

        /// <summary>
        /// Email format regex.
        /// </summary>
        public static readonly Regex Format = EmailFormatRegex();

        [GeneratedRegex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$", RegexOptions.Compiled)]
        private static partial Regex EmailFormatRegex();
    }

    /// <summary>
    /// Human related validation constants.
    /// </summary>
    public static class Person
    {
        /// <summary>
        /// Min name length.
        /// </summary>
        public const int MinNameLength = 2;

        /// <summary>
        /// Max name length.
        /// </summary>
        public const int MaxNameLength = 50;

        /// <summary>
        /// Max allowed person age.
        /// Currently the oldest person is 118 years old. And oldest verified record age is 122 years old.
        /// </summary>
        public const int MaxAge = 125;
    }
}
