namespace RpgBooks.Libraries.Module.Application.Queries.Page;

using RpgBooks.Libraries.Module.Application.Queries.Contracts;
using RpgBooks.Libraries.Module.Application.Results;
using RpgBooks.Libraries.Module.Application.Results.Contracts;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Base page query handler.
/// </summary>
/// <typeparam name="TQuery">Type of the query request.</typeparam>
/// <typeparam name="TPageItem">Type of the page response item.</typeparam>
public abstract class PageQueryHandler<TQuery, TPageItem> : IResultQueryHandler<TQuery, PageQueryResponseModel<TPageItem>>
    where TQuery : PageQueryModel
{
    private const int DefaultPageSize = 20;
    private const int DefaultPage = 1;

    /// <inheritdoc/>
    public async Task<IAppResult<PageQueryResponseModel<TPageItem>>> Handle(TQuery query, CancellationToken cancellation)
    {
        int pageSize = query.Size ?? DefaultPageSize;
        int pageNumber = query.Number ?? DefaultPage;

        var handleResult = await this.HandleQuery(query, cancellation);

        var pagedQuery = handleResult
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize);

        return AppResult.Success(string.Empty, new PageQueryResponseModel<TPageItem>(pageNumber, pageSize, pagedQuery));
    }

    /// <summary>
    /// Specific handle implementation called after query handle.
    /// </summary>
    /// <param name="request">Input command.</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>Completed task.</returns>
    public abstract Task<IOrderedQueryable<TPageItem>> HandleQuery(TQuery request, CancellationToken cancellation);
}
