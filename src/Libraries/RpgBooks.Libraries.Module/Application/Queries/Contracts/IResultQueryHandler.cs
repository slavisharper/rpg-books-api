namespace RpgBooks.Libraries.Module.Application.Queries.Contracts;

using RpgBooks.Libraries.Module.Application.Results.Contracts;

/// <summary>
/// Query request handler that returns specific <see cref="IAppResult{TResponseData}"/> result.
/// </summary>
/// <typeparam name="TQuery">Type of the query request.</typeparam>
/// <typeparam name="TResponseData">Type of the response data model.</typeparam>
public interface IResultQueryHandler<in TQuery, TResponseData> : IQueryHandler<TQuery, IAppResult<TResponseData>>
    where TQuery : IQuery
{
}
