namespace RpgBooks.Libraries.Module.Application.Queries.Page;

using System.Collections.Generic;

/// <summary>
/// Page query response model.
/// </summary>
/// <typeparam name="TPageItem">Type of the page item.</typeparam>
public sealed class PageQueryResponseModel<TPageItem>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PageQueryResponseModel{TPageItem}"/> class.
    /// </summary>
    /// <param name="number">Current page number.</param>
    /// <param name="size">Current page size.</param>
    /// <param name="items">Page items.</param>
    public PageQueryResponseModel(int number, int size, IQueryable<TPageItem> items)
    {
        this.Number = number;
        this.Size = size;
        this.Items = items.ToArray();
    }

    /// <inheritdoc/>
    public int Size { get; init; }

    /// <inheritdoc/>
    public int Number { get; init; }

    /// <inheritdoc/>
    public IReadOnlyCollection<TPageItem> Items { get; init; } = default!;
}
