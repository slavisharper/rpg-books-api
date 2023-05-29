namespace RpgBooks.Libraries.Module.Application.Queries.Page;

/// <summary>
/// Page query filter model.
/// </summary>
public sealed record PageQueryFilterModel
{
    /// <summary>
    /// Gets the filter value.
    /// </summary>
    public string? Filter { get; init; } = default!;

    /// <summary>
    /// Gets the filter key.
    /// </summary>
    public string FilterKey { get; init; } = default!;

    /// <summary>
    /// Gets the filter direction.
    /// </summary>
    public FilterDirection FilterDirection { get; init; } = FilterDirection.Ascending;
}
