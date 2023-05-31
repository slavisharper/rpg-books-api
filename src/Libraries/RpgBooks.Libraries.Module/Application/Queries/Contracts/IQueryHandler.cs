namespace RpgBooks.Libraries.Module.Application.Queries.Contracts;

/// <summary>
/// Query handler interface.
/// </summary>
/// <typeparam name="TQuery">Type of the query request.</typeparam>
/// <typeparam name="TQueryResult">Type of the query result.</typeparam>
public interface IQueryHandler<in TQuery, TQueryResult>
    where TQuery : IQuery
{
    /// <summary>
    /// Handles and processes given query request.
    /// </summary>
    /// <param name="query">Query request that will be handled.</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>Task with the query response model.</returns>
    Task<TQueryResult> Handle(TQuery query, CancellationToken cancellation);
}
