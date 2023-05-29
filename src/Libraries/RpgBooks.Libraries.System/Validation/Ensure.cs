namespace System;

using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

using static System.ValidationConstants;

/// <summary>
/// Guard class for argument validation.
/// </summary>
public static class Ensure
{
    /// <summary>
    /// Validates given value against given assertion function.
    /// </summary>
    /// <typeparam name="TValue">Type of the validated value.</typeparam>
    /// <typeparam name="TException">Type of the exception that will be thrown.</typeparam>
    /// <param name="value">Value that will be validated.</param>
    /// <param name="assertion">Assertion function that performs the check</param>
    /// <param name="errorMessage">Error message that will be displayed </param>
    public static void That<TValue, TException>(TValue? value, Func<TValue?, bool> assertion, string errorMessage)
        where TException : Exception, IValidationException, new()
    {
        if (!assertion.Invoke(value))
        {
            ThrowException<TException>(errorMessage);
        }
    }

    /// <summary>
    /// Checks if given value is not null. Throws exception if it is.
    /// </summary>
    public static void IsNotNull<TValue, TException>(TValue? value, [CallerArgumentExpression(nameof(value))] string paramName = "value", string? message = null)
        where TValue : class
        where TException : Exception, IValidationException, new()
        => That<TValue, TException>(
            value,
            val => val is not null,
            message ?? ValidationMessagesProvider.ForNullValue(paramName));

    /// <summary>
    /// Checks if given list of values is not empty. Throws exception if it null or empty.
    /// </summary>
    public static void IsNotEmpty<TValue, TException>(TValue value, [CallerArgumentExpression(nameof(value))] string paramName = "value", string? message = null)
        where TValue : IEnumerable<TValue>
        where TException : Exception, IValidationException, new()
        => That<TValue, TException>(
            value,
            val => val is not null && val.Any(),
            message ?? ValidationMessagesProvider.ForEmptyString(paramName));

    #region String
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TException">Type of the exception that will be thrown.</typeparam>
    /// <param name="value">Value that will be validated.</param>
    /// <param name="paramName">Name of the value.</param>
    /// <param name="message">Error message that will be displayed </param>
    public static void IsNotEmpty<TException>(string? value, [CallerArgumentExpression(nameof(value))] string paramName = "value", string? message = null)
        where TException : Exception, IValidationException, new()
        => That<string, TException>(
            value,
            val => val is not null && val.Any(),
            message ?? ValidationMessagesProvider.ForEmptyString(paramName));

    /// <summary>
    /// Validates if given string has min length.
    /// </summary>
    /// <typeparam name="TException">Type of the exception that will be thrown.</typeparam>
    /// <param name="value">Value that will be validated.</param>
    /// <param name="minLength">Minimum length that string should have.</param>
    /// <param name="paramName">Name of the value.</param>
    public static void HasMinLength<TException>(string? value, int minLength, [CallerArgumentExpression(nameof(value))] string paramName = "value")
        where TException : Exception, IValidationException, new()
        => That<string, TException>(
            value,
            val => val is not null && val.Length >= minLength,
            ValidationMessagesProvider.ForMinLength(paramName, minLength));

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TException">Type of the exception that will be thrown.</typeparam>
    /// <param name="value">Value that will be validated.</param>
    /// <param name="maxLength"></param>
    /// <param name="paramName">Name of the value.</param>
    public static void HasMaxLength<TException>(string? value, int maxLength, [CallerArgumentExpression(nameof(value))] string paramName = "value")
        where TException : Exception, IValidationException, new()
        => That<string, TException>(
            value,
            val => val is null || val.Length <= maxLength,
            ValidationMessagesProvider.ForMaxLength(paramName, maxLength));

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TException">Type of the exception that will be thrown.</typeparam>
    /// <param name="value">Value that will be validated.</param>
    /// <param name="format"></param>
    /// <param name="paramName">Name of the value.</param>
    public static void HasValidFormat<TException>(string value, Regex format, [CallerArgumentExpression(nameof(value))] string paramName = "value")
        where TException : Exception, IValidationException, new()
    {
        IsNotEmpty<TException>(value, paramName);
        if (!format.IsMatch(value))
        {
            ThrowException<TException>(ValidationMessagesProvider.ForWrongFormat(paramName));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TException">Type of the exception that will be thrown.</typeparam>
    /// <param name="email"></param>
    /// <param name="paramName">Name of the value.</param>
    public static void IsValidEmail<TException>(string email, [CallerArgumentExpression(nameof(email))] string paramName = "email")
        where TException : Exception, IValidationException, new()
    {
        HasMinLength<TException>(email, ValidationConstants.Email.MinLength, paramName);
        HasMaxLength<TException>(email, ValidationConstants.Email.MaxLength, paramName);
        HasValidFormat<TException>(email, ValidationConstants.Email.Format, paramName);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TException">Type of the exception that will be thrown.</typeparam>
    /// <param name="phone"></param>
    /// <param name="paramName">Name of the value.</param>
    public static void IsValidPhoneNumber<TException>(string phone, [CallerArgumentExpression(nameof(phone))] string paramName = "phone")
        where TException : Exception, IValidationException, new()
    {
        HasMinLength<TException>(phone, ValidationConstants.Phone.MinLength, paramName);
        HasMaxLength<TException>(phone, ValidationConstants.Phone.MaxLength, paramName);
        HasValidFormat<TException>(phone, ValidationConstants.Phone.Format, paramName);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TException"></typeparam>
    /// <param name="value">Value that will be validated.</param>
    /// <param name="paramName">Name of the value.</param>
    /// <param name="errorMessage"></param>
    public static void HasOnlyDigits<TException>(
        string value,
        [CallerArgumentExpression(nameof(value))] string paramName = "value",
        string? errorMessage = null)
            where TException : Exception, IValidationException, new()
            => That<string, TException>(
                value,
                val => val is not null && val.All(c => c >= '0' && c <= '9'),
                errorMessage ?? ValidationMessagesProvider.ForHasOnlyDigits(paramName));

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TException">Type of the exception that will be thrown.</typeparam>
    /// <param name="url"></param>
    /// <param name="paramName">Name of the value.</param>
    /// <param name="errorMessage"></param>
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
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="first"></param>
    /// <param name="second"></param>
    /// <param name="paramName"></param>
    /// <param name="secondParamName"></param>
    /// <param name="errorMessage"></param>
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

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="first"></param>
    /// <param name="second"></param>
    /// <param name="paramName"></param>
    /// <param name="errorMessage"></param>
    public static void IsLessThanOrEqualTo<TValue, TException>(TValue first, TValue second, string paramName, string? errorMessage = null)
        where TValue : IComparable<TValue>
        where TException : Exception, IValidationException, new()
        => That<TValue, TException>(
            first,
            val => val is not null && val.CompareTo(second) <= 0,
            errorMessage ?? ValidationMessagesProvider.ForLessThanOrEqualTo(paramName, second));

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="value"></param>
    /// <param name="paramName"></param>
    /// <param name="errorMessage"></param>
    public static void IsLessThanZero<T, TException>(T value, [CallerArgumentExpression(nameof(value))] string paramName = "value", string? errorMessage = null)
        where TException : Exception, IValidationException, new()
        where T : INumber<T>
        => IsLessThan<T, TException>(
            value,
            T.Zero,
            paramName,
            errorMessage ?? ValidationMessagesProvider.ForLessThanZero(paramName));
    
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="value"></param>
    /// <param name="paramName"></param>
    /// <param name="errorMessage"></param>
    public static void IsLessThanOrEqualToZero<T, TException>(T value, [CallerArgumentExpression(nameof(value))] string paramName = "value", string? errorMessage = null)
        where TException : Exception, IValidationException, new()
        where T : INumber<T>
        => IsLessThanOrEqualTo<T, TException>(
            value,
            T.Zero,
            errorMessage ?? ValidationMessagesProvider.ForLessThanOrEqualToZero(paramName));
    #endregion

    #region Greater Than
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="first"></param>
    /// <param name="second"></param>
    /// <param name="paramName"></param>
    /// <param name="errorMessage"></param>
    public static void IsGreaterThan<TValue, TException>(TValue first, TValue second, string paramName, string? errorMessage = null)
        where TValue : IComparable<TValue>
        where TException : Exception, IValidationException, new()
        => That<TValue, TException>(
            first,
            val => val is not null && val.CompareTo(second) > 0,
            errorMessage ?? ValidationMessagesProvider.ForGreaterThan(paramName, second));

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="first"></param>
    /// <param name="second"></param>
    /// <param name="paramName"></param>
    /// <param name="errorMessage"></param>
    public static void IsGreaterThanOrEqualTo<TValue, TException>(TValue first, TValue second, string paramName, string? errorMessage = null)
        where TValue : IComparable<TValue>
        where TException : Exception, IValidationException, new()
        => That<TValue, TException>(
            first,
            val => val is not null && val.CompareTo(second) >= 0,
            errorMessage ?? ValidationMessagesProvider.ForGreaterThanOrEqualTo(paramName, second));
    
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="value"></param>
    /// <param name="paramName"></param>
    /// <param name="errorMessage"></param>
    public static void IsGreaterThanZero<T, TException>(T value, [CallerArgumentExpression(nameof(value))] string paramName = "value", string? errorMessage = null)
        where TException : Exception, IValidationException, new()
        where T : INumber<T>
        => IsGreaterThan<T, TException>(
            value,
            T.Zero,
            paramName,
            errorMessage ?? ValidationMessagesProvider.ForGreaterThanZero(paramName));
    
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TException"></typeparam>
    /// <param name="value"></param>
    /// <param name="paramName"></param>
    /// <param name="errorMessage"></param>
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
