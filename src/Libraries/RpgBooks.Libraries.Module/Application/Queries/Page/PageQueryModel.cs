namespace RpgBooks.Libraries.Module.Application.Queries.Page;

using RpgBooks.Libraries.Module.Application.Queries.Contracts;

/// <summary>
/// Default page query request model.
/// </summary>
public abstract record PageQueryModel : IQuery
{
    /// <inheritdoc/>
    public int? Size { get; init; }

    /// <inheritdoc/>
    public int? Number { get; init; }

    /// <inheritdoc/>
    public IEnumerable<PageQueryFilterModel>? Filters { get; init; }
}
