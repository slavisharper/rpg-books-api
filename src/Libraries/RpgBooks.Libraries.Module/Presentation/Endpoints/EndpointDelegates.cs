namespace RpgBooks.Libraries.Module.Presentation.Endpoints;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using RpgBooks.Libraries.Module.Application.Commands.Contracts;
using RpgBooks.Libraries.Module.Application.Queries.Contracts;
using RpgBooks.Libraries.Module.Application.Queries.Page;
using RpgBooks.Libraries.Module.Application.Results.Contracts;
using RpgBooks.Libraries.Module.Presentation.Endpoints.Extensions;

using System.Threading;

using IAppResult = RpgBooks.Libraries.Module.Application.Results.Contracts.IAppResult;

/// <summary>
/// Endpoint delegates that can be used for registering endpoints with predefined delegates.
/// </summary>
public static class EndpointDelegates
{
    /// <summary>
    /// Delegate used for endpoints that are calling a query handler.
    /// </summary>
    /// <typeparam name="TRequest">Type of the query request.</typeparam>
    /// <typeparam name="TData">Type of the query handler response data.</typeparam>
    /// <returns>Endpoint handler delegate.</returns>
    public static Delegate QueryHandlerDelegate<TRequest, TData>()
        where TRequest : IQuery
        => async ([FromServices] IQueryHandlerDispatcher dispatcher, [AsParameters] TRequest query, CancellationToken cancellationToken)
            => await dispatcher.Dispatch<TRequest, IAppResult<TData>>(query!, cancellationToken).ToIResult();

    /// <summary>
    /// Delegate used for endpoints that are calling a page query handler.
    /// </summary>
    /// <typeparam name="TRequest">Type of the query request.</typeparam>
    /// <typeparam name="TItem">Type of the page item.</typeparam>
    /// <returns>Endpoint handler delegate.</returns>
    public static Delegate PageQueryHandlerDelegate<TRequest, TItem>()
        where TRequest : PageQueryModel
        => async ([FromServices] IQueryHandlerDispatcher dispatcher, [AsParameters] TRequest query, CancellationToken cancellationToken)
            => await dispatcher.Dispatch<TRequest, IAppResult<PageQueryResponseModel<TItem>>>(query!, cancellationToken).ToIResult();

    /// <summary>
    /// Delegate used for endpoints that are calling a command handler that will create a new entity.
    /// </summary>
    /// <typeparam name="TRequest">Type of the command request.</typeparam>
    /// <returns>Endpoint handler delegate.</returns>
    public static Delegate CommonCommandHandlerDelegate<TRequest>()
        where TRequest : ICommand
        => async ([FromServices] ICommandHandlerDispatcher dispatcher, [FromBody] TRequest request, CancellationToken cancellationToken)
            => await dispatcher.Dispatch<TRequest, IAppResult>(request!, cancellationToken).ToIResult();

    /// <summary>
    /// Delegate used for endpoints that are calling a command handler that will create a new entity and return a given result.
    /// </summary>
    /// <typeparam name="TRequest">Type of the command request.</typeparam>
    /// <typeparam name="TResponseData">Type of the response data contained in the HandlerResult.</typeparam>
    /// <returns>Endpoint handler delegate.</returns>
    public static Delegate CommonCommandHandlerDelegate<TRequest, TResponseData>()
        where TRequest : ICommand
        => async ([FromServices] ICommandHandlerDispatcher dispatcher, [FromBody] TRequest request, CancellationToken cancellationToken)
            => await dispatcher.Dispatch<TRequest, IAppResult<TResponseData>>(request!, cancellationToken).ToIResult();

    /// <summary>
    /// Delegate used for commands that are having a custom binding model.
    /// This commands implement BindAsync method that is used to bind the request model.
    /// This is very useful for multi-part form data requests and having file upload.
    /// </summary>
    /// <typeparam name="TRequest">Type of the request model.</typeparam>
    /// <returns>Endpoint handler delegate.</returns>
    public static Delegate CustomBindedCommandHandlerDelegate<TRequest>()
        where TRequest : ICommand
        => async ([FromServices] ICommandHandlerDispatcher dispatcher, TRequest request, CancellationToken cancellationToken)
            => await dispatcher.Dispatch<TRequest, IAppResult>(request!, cancellationToken).ToIResult();

    /// <summary>
    /// Delegate used for commands that are having a custom binding model.
    /// This commands implement BindAsync method that is used to bind the request model.
    /// This is very useful for multi-part form data requests and having file upload.
    /// </summary>
    /// <typeparam name="TRequest">Type of the request model.</typeparam>
    /// <typeparam name="TResponseData">Response data.</typeparam>
    /// <returns>Endpoint handler delegate.</returns>
    public static Delegate CustomBindedCommandHandlerDelegate<TRequest, TResponseData>()
        where TRequest : ICommand
        => async ([FromServices] ICommandHandlerDispatcher dispatcher, TRequest request, CancellationToken cancellationToken)
            => await dispatcher.Dispatch<TRequest, IAppResult<TResponseData>>(request!, cancellationToken).ToIResult();

    /// <summary>
    /// Delegate for Id only commands that is getting the Id value from the route.
    /// </summary>
    /// <typeparam name="TKey">Type of the identifier passed with the request.</typeparam>
    /// <typeparam name="TRequest">Type of the request model.</typeparam>
    /// <returns>Endpoint handler delegate.</returns>
    public static Delegate UpdateCommandHandlerDelegate<TKey, TRequest>()
        where TRequest : IUpdateCommand<TKey>, new()
        => async ([FromServices] ICommandHandlerDispatcher dispatcher, [FromRoute] TKey id, [FromBody] TRequest request, CancellationToken cancellationToken) =>
        {
            request ??= new();
            request.SetId(id);
            return await dispatcher.Dispatch<TRequest, IAppResult>(request, cancellationToken).ToIResult();
        };

    /// <summary>
    /// Delegate for Id only commands that is getting the Id value from the route.
    /// </summary>
    /// <typeparam name="TKey">Type of the identifier passed with the request.</typeparam>
    /// <typeparam name="TRequest">Type of the request model.</typeparam>
    /// <typeparam name="TResponseData">Type of the response model.</typeparam>
    /// <returns>Endpoint handler delegate.</returns>
    public static Delegate UpdateCommandHandlerDelegate<TKey, TRequest, TResponseData>()
        where TRequest : IUpdateCommand<TKey>, new()
        => async ([FromServices] ICommandHandlerDispatcher dispatcher, [FromRoute] TKey id, [FromBody] TRequest request, CancellationToken cancellationToken) =>
        {
            request ??= new();
            request.SetId(id);
            return await dispatcher.Dispatch<TRequest, IAppResult<TResponseData>>(request, cancellationToken).ToIResult();
        };

    /// <summary>
    /// Command handler getting the request model from the request body.
    /// </summary>
    /// <typeparam name="TKey">Type of the identifier passed with the request.</typeparam>
    /// <typeparam name="TRequest">Type of the request model.</typeparam>
    /// <returns>Endpoint handler delegate.</returns>
    public static Delegate DeleteCommandHandlerDelegate<TKey, TRequest>()
        where TRequest : IUpdateCommand<TKey>, new()
        => async ([FromServices] ICommandHandlerDispatcher dispatcher, [FromRoute] TKey id, [AsParameters] TRequest request, CancellationToken cancellationToken) =>
        {
            request ??= new();
            request.SetId(id);
            return await dispatcher.Dispatch<TRequest, IAppResult>(request, cancellationToken).ToIResult();
        };
}
