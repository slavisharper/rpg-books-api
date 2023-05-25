namespace System.Collections.Generic;

using System.Collections.Generic;

/// <summary>
/// Defines extension methods on the <see cref="IList{T}"/> interface.
/// </summary>
public static class IListExtensions
{
    /// <summary>
    /// Add range of items into given list.
    /// </summary>
    /// <typeparam name="T">List item type.</typeparam>
    /// <param name="collection">List instance.</param>
    /// <param name="items">Items to add.</param>
    public static void AddRange<T>(this IList<T> collection, IEnumerable<T> items)
    {
        if (items.IsNotEmpty())
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }
}
