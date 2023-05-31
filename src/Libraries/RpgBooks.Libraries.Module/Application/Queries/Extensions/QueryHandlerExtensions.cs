namespace RpgBooks.Libraries.Module.Application.Queries.Extensions;

using RpgBooks.Libraries.Module.Application.Queries.Contracts;
using RpgBooks.Libraries.Module.Application.Results;
using RpgBooks.Libraries.Module.Application.Results.Contracts;

/// <summary>
/// Extension methods for <see cref="QueryHandlerExtensions"/>.
/// </summary>
public static class QueryHandlerExtensions
{
    /// <summary>
    /// Creates successful handler result.
    /// </summary>
    /// <typeparam name="TQuery">Type of the query request.</typeparam>
    /// <typeparam name="TResponseData">Type of the response data model.</typeparam>
    /// <param name="handler">Handler instance.</param>
    /// <param name="message">Success message.</param>
    /// <param name="responseData">Response data model.</param>
    /// <returns>Created success result.</returns>
    public static IAppResult<TResponseData> Success<TQuery, TResponseData>(this IQueryHandler<TQuery, IAppResult<TResponseData>> handler, string message, TResponseData responseData)
        where TQuery : IQuery
        => AppResult.Success(message, responseData);

    /// <summary>
    /// Creates handler result.
    /// </summary>
    /// <typeparam name="TQuery">Type of the query request.</typeparam>
    /// <typeparam name="TResponseData">Type of the response data model.</typeparam>
    /// <param name="handler">Handler instance.</param>
    /// <param name="message">message.</param>
    /// <returns>Created result.</returns>
    public static IAppResult<TResponseData> NotFound<TQuery, TResponseData>(this IQueryHandler<TQuery, IAppResult<TResponseData>> handler, string message)
        where TQuery : IQuery
        => AppResult.NotFound<TResponseData>(message);

    /// <summary>
    /// Creates handler result.
    /// </summary>
    /// <typeparam name="TQuery">Type of the query request.</typeparam>
    /// <typeparam name="TResponseData">Type of the response data model.</typeparam>
    /// <param name="handler">Handler instance.</param>
    /// <param name="message">message.</param>
    /// <returns>Created result.</returns>
    public static IAppResult<TResponseData> Unauthorized<TQuery, TResponseData>(this IQueryHandler<TQuery, IAppResult<TResponseData>> handler, string message)
        where TQuery : IQuery
        => AppResult.Unauthorized<TResponseData>(message);
}
