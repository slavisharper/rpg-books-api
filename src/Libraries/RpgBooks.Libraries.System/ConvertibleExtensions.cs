namespace System;

/// <summary>
/// Extension methods for <see cref="IConvertible"/>.
/// </summary>
public static class ConvertibleExtensions
{
    /// <summary>
    /// Converts the value of this instance to an object of the specified type.
    /// </summary>
    /// <typeparam name="T">Type to which the object will be converted.</typeparam>
    /// <param name="obj">Object that will be converted.</param>
    /// <returns>Converted object.</returns>
    public static T To<T>(this IConvertible obj)
    {
        return (T)Convert.ChangeType(obj, typeof(T));
    }
}