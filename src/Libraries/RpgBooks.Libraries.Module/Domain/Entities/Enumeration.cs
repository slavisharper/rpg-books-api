namespace RpgBooks.Libraries.Module.Domain.Entities;

using Cysharp.Text;

using global::System;
using global::System.Collections.Concurrent;
using global::System.Reflection;
using global::System.Runtime.CompilerServices;

/// <summary>
/// Enumeration class.
/// </summary>
public abstract class Enumeration : IComparable
{
    private static readonly ConcurrentDictionary<Type, IEnumerable<object>> EnumCache = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="Enumeration"/> class.
    /// </summary>
    /// <param name="value">Enumeration value.</param>
    /// <param name="name">Enumeration name.</param>
    protected Enumeration(int value, string name)
    {
        Value = value;
        Name = name;
    }

    /// <summary>
    /// Gets enumeration value.
    /// </summary>
    public int Value { get; }

    /// <summary>
    /// Gets enumeration name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Equality comparer operator.
    /// </summary>
    /// <param name="left">Compared enumeration value.</param>
    /// <param name="right">Comparer enumeration value.</param>
    /// <returns>True if the two enumerations are equal.</returns>
    public static bool operator ==(Enumeration? left, Enumeration? right)
    {
        return EqualityComparer<Enumeration>.Default.Equals(left, right);
    }

    /// <summary>
    /// Not equality comparer operator.
    /// </summary>
    /// <param name="left">Compared enumeration value.</param>
    /// <param name="right">Comparer enumeration value.</param>
    /// <returns>True if the two enumerations are not equal.</returns>
    public static bool operator !=(Enumeration? left, Enumeration? right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Gets an enumerable list of all elements in the enumeration.
    /// </summary>
    /// <typeparam name="T">Type of enumeration.</typeparam>
    /// <returns>Returns an enumerable list of all elements in the enumeration.</returns>
    public static IEnumerable<T> GetAll<T>()
        where T : Enumeration
    {
        var type = typeof(T);

        var values = EnumCache.GetOrAdd(type, _ => type
            .GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Select(f => f.GetValue(null))
            .Cast<T>());

        return values.Cast<T>();
    }

    /// <summary>
    /// Gets an enumerable list of all elements in the enumeration.
    /// </summary>
    /// <param name="enumType">Enumeration type.</param>
    /// <returns>Returns an enumerable list of all elements in the enumeration.</returns>
    public static IEnumerable<Enumeration> GetAll(Type enumType)
    {
        var values = EnumCache.GetOrAdd(enumType, _ => enumType
            .GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Select(f => f.GetValue(null))!);

        return values.Select(v => (Enumeration)v);
    }

    /// <summary>
    /// Gets enumeration object by specified enumeration type and value.
    /// </summary>
    /// <typeparam name="T">Enumeration type.</typeparam>
    /// <param name="value">Enumeration value.</param>
    /// <returns>Returns enumeration object by specified enumeration type and value.</returns>
    public static T FromValue<T>(int value)
        where T : Enumeration
        => Parse<T, int>(value, item => item.Value == value);

    /// <summary>
    /// Gets enumeration object by specified enumeration type and name.
    /// </summary>
    /// <typeparam name="T">Enumeration type.</typeparam>
    /// <param name="name">Enumeration name.</param>
    /// <returns>Returns enumeration object by specified enumeration type and name.</returns>
    public static T FromName<T>(string name)
        where T : Enumeration
        => Parse<T, string>(name, item => item.Name == name);

    /// <summary>
    /// Gets enumeration object by specified enumeration type and name.
    /// </summary>
    /// <param name="enumType">>Enumeration type.</param>
    /// <param name="name">Enumeration name.</param>
    /// <returns>Returns enumeration object by specified enumeration type and name.</returns>
    public static Enumeration FromName(Type enumType, string name)
        => Parse(enumType, name, item => item.Name == name);

    /// <summary>
    /// Gets enumeration name by specified enumeration type and value.
    /// </summary>
    /// <typeparam name="T">Enumeration type.</typeparam>
    /// <param name="value">Enumeration value.</param>
    /// <returns>Returns enumeration name by specified enumeration type and value.</returns>
    public static string NameFromValue<T>(int value)
        where T : Enumeration
        => FromValue<T>(value).Name;

    /// <summary>
    /// Indicates if enumeration has specific value.
    /// </summary>
    /// <typeparam name="T">Enumeration type.</typeparam>
    /// <param name="value">Enumeration value.</param>
    /// <returns>Returns if enumeration has specific value.</returns>
    public static bool HasValue<T>(int value)
        where T : Enumeration
    {
        try
        {
            FromValue<T>(value);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Test.
    /// </summary>
    /// <typeparam name="TEnumeration">Enumeration.</typeparam>
    /// <param name="valueOrName">Value or name input.</param>
    /// <param name="enumeration">Enumeration output.</param>
    /// <returns>Whether <paramref name="valueOrName"/> is parsed as valid enumeration type.</returns>
    public static bool TryGetFromValueOrName<TEnumeration>(
        string valueOrName,
        out TEnumeration enumeration)
        where TEnumeration : Enumeration
        => TryParse(item => item.Name == valueOrName, out enumeration!)
        || int.TryParse(valueOrName, out var value)
        && TryParse(item => item.Value == value, out enumeration!);

    /// <inheritdoc/>
    public override string ToString()
        => Name;

    /// <summary>
    /// Get enumeration item value.
    /// </summary>
    /// <returns>Returns enumeration item value.</returns>
    public int ToValue()
        => Value;

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        if (obj is not Enumeration otherValue)
        {
            return false;
        }

        var typeMatches = GetType() == obj.GetType();
        var valueMatches = Value.Equals(otherValue.Value);

        return typeMatches && valueMatches;
    }

    /// <inheritdoc/>
    public override int GetHashCode()
        => (GetType().ToString() + Value).GetHashCode();

    /// <summary>
    /// Compare two enumeration objects.
    /// </summary>
    /// <param name="other">Compare against this object.</param>
    /// <returns>
    /// <para>Return Value – Description</para>
    /// <para>Less than zero – This instance is less than value.</para>
    /// <para>Zero – This instance is equal to value.</para>
    /// <para>Greater than zero – This instance is greater than value.</para>
    /// </returns>
    public int CompareTo(object? other)
        => Value.CompareTo(((Enumeration)other!).Value);

    /// <summary>
    /// Copy the current enumeration to a new instance.
    /// </summary>
    /// <typeparam name="T">Type of the enumeration.</typeparam>
    /// <returns>Copied enumeration value.</returns>
    public T Copy<T>()
        where T: Enumeration
        => FromValue<T>(this.Value);

    private static T Parse<T, TValue>(
        TValue value,
        Func<T, bool> predicate,
        [CallerArgumentExpression(nameof(value))] string description = "?")
        where T : Enumeration
    {
        var matchingItem = GetAll<T>().FirstOrDefault(predicate) ??
            throw new InvalidOperationException(
                ZString.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T)));

        return matchingItem;
    }

    private static Enumeration Parse<TValue>(
        Type enumType,
        TValue value,
        Func<Enumeration, bool> predicate,
        [CallerArgumentExpression(nameof(value))] string description = "?")
    {
        var matchingItem = GetAll(enumType).FirstOrDefault(predicate) ??
            throw new InvalidOperationException(
                ZString.Format($"'{value}' is not a valid {description} in {enumType}", value, description));

        return matchingItem;
    }

    private static bool TryParse<TEnumeration>(
        Func<TEnumeration, bool> predicate,
        out TEnumeration? enumeration)
        where TEnumeration : Enumeration
    {
        enumeration = GetAll<TEnumeration>().FirstOrDefault(predicate);
        return enumeration != null;
    }
}
