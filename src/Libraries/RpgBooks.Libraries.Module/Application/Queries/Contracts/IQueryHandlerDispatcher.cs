namespace RpgBooks.Libraries.Module.Application.Queries.Contracts;

/// <summary>
/// Query request dispatcher.
/// </summary>
public interface IQueryHandlerDispatcher
{
    /// <summary>
    /// Dispatch a query request and calls handler that will handle the current query request.
    /// </summary>
    /// <typeparam name="TQuery">Type of the query request.</typeparam>
    /// <typeparam name="TQueryResult">Type of the query response.</typeparam>
    /// <param name="query">Query request input.</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>Value task with the query response.</returns>
    Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation)
        where TQuery : IQuery;
}
