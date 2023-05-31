namespace System;

using Cysharp.Text;

/// <summary>
/// Validation messages provider for <see cref="ValidationMessages"/>.
/// </summary>
public sealed class ValidationMessagesProvider
{
    internal static string ForNullValue(string paramName)
        => ZString.Format(ValidationMessages.NullValueErrorMessageFormat, paramName);

    internal static string ForEmptyString(string paramName)
        => ZString.Format(ValidationMessages.EmptyStringErrorMessageFormat, paramName);

    internal static string ForWrongFormat(string paramName)
        => ZString.Format(ValidationMessages.WrongFormatErrorMessageFormat, paramName);

    internal static string ForMaxLength(string paramName, int maxLength)
        => ZString.Format(ValidationMessages.MaxLengthErrorMessageFormat, paramName, maxLength);

    internal static string ForMinLength(string paramName, int minLength)
        => ZString.Format(ValidationMessages.MinLengthErrorMessageFormat, paramName, minLength);

    internal static string ForLessThan<TValue>(string paramName, TValue second) where TValue : IComparable<TValue>
        => ZString.Format(ValidationMessages.LessThanErrorMessageFormat, paramName, second);

    internal static string ForLessThanOrEqualTo<TValue>(string paramName, TValue second) where TValue : IComparable<TValue>
        => ZString.Format(ValidationMessages.LessThanOrEqualToErrorMessageFormat, paramName, second);

    internal static string ForLessThanZero(string paramName)
        => ZString.Format(ValidationMessages.LessThanZeroErrorMessageFormat, paramName);

    internal static string ForLessThanOrEqualToZero(string paramName)
        => ZString.Format(ValidationMessages.ForLessThanOrEqualToZeroErrorMessageFormat, paramName);

    internal static string ForGreaterThan<TValue>(string paramName, TValue second) where TValue : IComparable<TValue>
        => ZString.Format(ValidationMessages.GreaterThanErrorMessageFormat, paramName, second);

    internal static string ForGreaterThanOrEqualTo<TValue>(string paramName, TValue second) where TValue : IComparable<TValue>
        => ZString.Format(ValidationMessages.ForGreaterThanOrEqualToErrorMessageFormat, paramName, second);

    internal static string ForGreaterThanZero(string paramName)
        => ZString.Format(ValidationMessages.ForGreaterThanZeroErrorMessageFormat, paramName);

    internal static string ForGreaterThanOrEqualToZero(string paramName)
        => ZString.Format(ValidationMessages.ForHasOnlyDigitsErrorMessageFormat, paramName);

    internal static string ForHasOnlyDigits(string paramName)
        => ZString.Format(ValidationMessages.ForHasOnlyDigitsErrorMessageFormat, paramName);

    internal static string ForIsValidUrl(string paramName)
        => ZString.Format(ValidationMessages.ForIsValidUrlErrorMessageFormat, paramName);
}
