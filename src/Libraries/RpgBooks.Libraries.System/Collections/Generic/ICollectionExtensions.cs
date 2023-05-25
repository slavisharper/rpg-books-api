namespace System.Collections.Generic;

using System.Collections.Generic;

/// <summary>
/// Defines extension methods on the <see cref="ICollection{T}"/> interface.
/// </summary>
public static class ICollectionExtensions
{
    /// <summary>
    /// Add range of items into given collection.
    /// </summary>
    /// <typeparam name="T">Collection item type.</typeparam>
    /// <param name="collection">Collection instance.</param>
    /// <param name="items">Items to add.</param>
    public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            collection.Add(item);
        }
    }
}
