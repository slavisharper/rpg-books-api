namespace System.Collections.Generic;

using System.Collections.Generic;

/// <summary>
/// Defines extension methods on the <see cref="IEnumerable{T}"/> interface.
/// </summary>
public static class IEnumerableExtensions
{
    /// <summary>
    /// Indicates whether the specified collection is null or has no elements.
    /// </summary>
    /// <typeparam name="T">Type of the elements in the collection.</typeparam>
    /// <param name="collection">Source collection.</param>
    /// <returns>True if the collection is null or empty; otherwise false.</returns>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
    {
        return collection is null || !collection.Any();
    }

    /// <summary>
    /// Check if IEnumerable is not empty.
    /// </summary>
    /// <typeparam name="T">Type of the elements in the list.</typeparam>
    /// <param name="collection">List to be checked.</param>
    /// <returns>True if the collection has at least one item.</returns>
    public static bool IsNotEmpty<T>(this IEnumerable<T> collection)
    {
        return !collection.IsNullOrEmpty();
    }

    /// <summary>
    /// Performs the specified <paramref name="action"/> on each element of the Enumerable.
    /// </summary>
    /// <typeparam name="T">Enumerable type.</typeparam>
    /// <param name="source">Enumerable instance.</param>
    /// <param name="action">Action to perform on each item.</param>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        foreach (T element in source)
        {
            action(element);
        }
    }

    /// <summary>
    /// Determines whether the collection contains duplicate items.
    /// </summary>
    /// <typeparam name="T">Item type.</typeparam>
    /// <param name="source">Source enumerable.</param>
    /// <param name="equalityComparer">Custom equality comparer.</param>
    /// <returns>True if duplated items are found. False if collection is empty or unique.</returns>
    public static bool HasDuplicates<T>(this IEnumerable<T> source, IEqualityComparer<T>? equalityComparer = null)
    {
        if (source.IsNullOrEmpty())
        {
            return false;
        }

        return source.Count() != source.Distinct(equalityComparer).Count();
    }
}
