namespace System;

using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

using static System.ValidationConstants;

public static class Ensure
{
    public static void That<TValue, TException>(TValue? value, Func<TValue?, bool> assertion, string errorMessage)
        where TException : Exception, IValidationException, new()
    {
        if (!assertion.Invoke(value))
        {
            ThrowException<TException>(errorMessage);
        }
    }

    public static void IsNotNull<TValue, TException>(TValue? value, [CallerArgumentExpression(nameof(value))] string paramName = "value", string? message = null)
        where TValue : class
        where TException : Exception, IValidationException, new()
        => That<TValue, TException>(
            value,
            val => val is not null,
            message ?? ValidationMessagesProvider.ForNullValue(paramName));

    public static void IsNotEmpty<TValue, TException>(TValue value, [CallerArgumentExpression(nameof(value))] string paramName = "value", string? message = null)
        where TValue : IEnumerable<TValue>
        where TException : Exception, IValidationException, new()
        => That<TValue, TException>(
            value,
            val => val is not null && val.Any(),
            message ?? ValidationMessagesProvider.ForEmptyString(paramName));

    #region String
    public static void IsNotEmpty<TException>(string? value, [CallerArgumentExpression(nameof(value))] string paramName = "value", string? message = null)
        where TException : Exception, IValidationException, new()
        => That<string, TException>(
            value,
            val => val is not null && val.Any(),
            message ?? ValidationMessagesProvider.ForEmptyString(paramName));

    public static void HasMinLength<TException>(string? value, int minLength, [CallerArgumentExpression(nameof(value))] string paramName = "value")
        where TException : Exception, IValidationException, new()
        => That<string, TException>(
            value,
            val => val is not null && val.Length >= minLength,
            ValidationMessagesProvider.ForMinLength(paramName, minLength));

    public static void HasMaxLength<TException>(string? value, int maxLength, [CallerArgumentExpression(nameof(value))] string paramName = "value")
        where TException : Exception, IValidationException, new()
        => That<string, TException>(
            value,
            val => val is null || val.Length <= maxLength,
            ValidationMessagesProvider.ForMaxLength(paramName, maxLength));

    public static void HasValidFormat<TException>(string value, Regex format, [CallerArgumentExpression(nameof(value))] string paramName = "value")
        where TException : Exception, IValidationException, new()
    {
        IsNotEmpty<TException>(value, paramName);
        if (!format.IsMatch(value))
        {
            ThrowException<TException>(ValidationMessagesProvider.ForWrongFormat(paramName));
        }
    }

    public static void IsValidEmail<TException>(string email, [CallerArgumentExpression(nameof(email))] string paramName = "email")
        where TException : Exception, IValidationException, new()
    {
        HasMinLength<TException>(email, ValidationConstants.Email.MinLength, paramName);
        HasMaxLength<TException>(email, ValidationConstants.Email.MaxLength, paramName);
        HasValidFormat<TException>(email, ValidationConstants.Email.Format, paramName);
    }

    public static void IsValidPhoneNumber<TException>(string phone, [CallerArgumentExpression(nameof(phone))] string paramName = "phone")
        where TException : Exception, IValidationException, new()
    {
        HasMinLength<TException>(phone, ValidationConstants.Phone.MinLength, paramName);
        HasMaxLength<TException>(phone, ValidationConstants.Phone.MaxLength, paramName);
        HasValidFormat<TException>(phone, ValidationConstants.Phone.Format, paramName);
    }

    public static void HasOnlyDigits<TException>(
        string value,
        [CallerArgumentExpression(nameof(value))] string paramName = "value",
        string? errorMessage = null)
            where TException : Exception, IValidationException, new()
            => That<string, TException>(
                value,
                val => val is not null && val.All(c => c >= '0' && c <= '9'),
                errorMessage ?? ValidationMessagesProvider.ForHasOnlyDigits(paramName));

    public static void IsValidUrl<TException>(
        string url,
        [CallerArgumentExpression(nameof(url))] string paramName = "url",
        string? errorMessage = null)
            where TException : Exception, IValidationException, new()
            => That<string, TException>(
                url,
                v => url is not null && url.Length <= 2048 && Uri.IsWellFormedUriString(url, UriKind.Absolute),
                errorMessage ?? ValidationMessagesProvider.ForIsValidUrl(paramName));

    #endregion

    #region Is Less Than
    public static void IsLessThan<TValue, TException>(
        TValue first,
        TValue second,
        [CallerArgumentExpression(nameof(first))] string paramName = "first",
        [CallerArgumentExpression(nameof(second))] string secondParamName = "second",
        string? errorMessage = null)
            where TValue : IComparable<TValue>
            where TException : Exception, IValidationException, new()
            => That<TValue, TException>(
                first,
                val => val is not null && val.CompareTo(second) < 0,
                errorMessage ?? ValidationMessagesProvider.ForLessThan(paramName, secondParamName));


    public static void IsLessThanOrEqualTo<TValue, TException>(TValue first, TValue second, string paramName, string? errorMessage = null)
        where TValue : IComparable<TValue>
        where TException : Exception, IValidationException, new()
        => That<TValue, TException>(
            first,
            val => val is not null && val.CompareTo(second) <= 0,
            errorMessage ?? ValidationMessagesProvider.ForLessThanOrEqualTo(paramName, second));

    public static void IsLessThanZero<T, TException>(T value, [CallerArgumentExpression(nameof(value))] string paramName = "value", string? errorMessage = null)
        where TException : Exception, IValidationException, new()
        where T : INumber<T>
        => IsLessThan<T, TException>(
            value,
            T.Zero,
            paramName,
            errorMessage ?? ValidationMessagesProvider.ForLessThanZero(paramName));
    
    public static void IsLessThanOrEqualToZero<T, TException>(T value, [CallerArgumentExpression(nameof(value))] string paramName = "value", string? errorMessage = null)
        where TException : Exception, IValidationException, new()
        where T : INumber<T>
        => IsLessThanOrEqualTo<T, TException>(
            value,
            T.Zero,
            errorMessage ?? ValidationMessagesProvider.ForLessThanOrEqualToZero(paramName));
    #endregion

    #region Greater Than
    public static void IsGreaterThan<TValue, TException>(TValue first, TValue second, string paramName, string? errorMessage = null)
        where TValue : IComparable<TValue>
        where TException : Exception, IValidationException, new()
        => That<TValue, TException>(
            first,
            val => val is not null && val.CompareTo(second) > 0,
            errorMessage ?? ValidationMessagesProvider.ForGreaterThan(paramName, second));

    public static void IsGreaterThanOrEqualTo<TValue, TException>(TValue first, TValue second, string paramName, string? errorMessage = null)
        where TValue : IComparable<TValue>
        where TException : Exception, IValidationException, new()
        => That<TValue, TException>(
            first,
            val => val is not null && val.CompareTo(second) >= 0,
            errorMessage ?? ValidationMessagesProvider.ForGreaterThanOrEqualTo(paramName, second));
    
    public static void IsGreaterThanZero<T, TException>(T value, [CallerArgumentExpression(nameof(value))] string paramName = "value", string? errorMessage = null)
        where TException : Exception, IValidationException, new()
        where T : INumber<T>
        => IsGreaterThan<T, TException>(
            value,
            T.Zero,
            paramName,
            errorMessage ?? ValidationMessagesProvider.ForGreaterThanZero(paramName));
    
    public static void IsGreaterThanOrEqualToZero<T, TException>(T value, [CallerArgumentExpression(nameof(value))] string paramName = "value", string? errorMessage = null)
        where TException : Exception, IValidationException, new()
        where T : INumber<T>
        => IsGreaterThanOrEqualTo<T, TException>(
            value,
            T.Zero,
            paramName,
            errorMessage ?? ValidationMessagesProvider.ForGreaterThanOrEqualToZero(paramName));
    #endregion

    private static void ThrowException<TException>(string message)
        where TException : Exception, IValidationException, new()
    {
        var ex = new TException
        {
            ValidationMessage = message,
        };

        throw ex;
    }
}
