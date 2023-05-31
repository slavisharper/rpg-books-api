namespace RpgBooks.Libraries.Module.Application.Queries.Page;

/// <summary>
/// Paged query filter order direction.
/// <para>This tells the query handler how to order the result.</para>
/// </summary>
public enum FilterDirection
{
    /// <summary>
    /// Ascending order.
    /// </summary>
    Ascending = 0,

    /// <summary>
    /// Descending order.
    /// </summary>
    Descending = 1
}
