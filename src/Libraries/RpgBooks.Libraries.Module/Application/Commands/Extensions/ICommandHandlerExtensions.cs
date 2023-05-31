namespace RpgBooks.Libraries.Module.Application.Commands.Extensions;

using RpgBooks.Libraries.Module.Application.Commands.Contracts;
using RpgBooks.Libraries.Module.Application.Results;
using RpgBooks.Libraries.Module.Application.Results.Contracts;

/// <summary>
/// Extension methods for <see cref="ICommandHandler{TCommand, TResponse}"/>.
/// </summary>
public static class ICommandHandlerExtensions
{
    /// <summary>
    /// Creates successful handler result.
    /// </summary>
    /// <typeparam name="TCommand">Type of the query request.</typeparam>
    /// <typeparam name="TResponseData">Type of the response data model.</typeparam>
    /// <param name="handler">Handler instance.</param>
    /// <param name="message">Success message.</param>
    /// <param name="responseData">Response data model.</param>
    /// <returns>Created success result.</returns>
    public static IAppResult<TResponseData> Success<TCommand, TResponseData>(this ICommandHandler<TCommand, IAppResult<TResponseData>> handler, string message, TResponseData responseData)
        where TCommand : ICommand
        => AppResult.Success(message, responseData);

    /// <summary>
    /// Creates handler result.
    /// </summary>
    /// <typeparam name="TCommand">Type of the query request.</typeparam>
    /// <typeparam name="TResponseData">Type of the response data model.</typeparam>
    /// <param name="handler">Handler instance.</param>
    /// <param name="message">message.</param>
    /// <returns>Created result.</returns>
    public static IAppResult<TResponseData> Failure<TCommand, TResponseData>(this ICommandHandler<TCommand, IAppResult<TResponseData>> handler, string message)
        where TCommand : ICommand
        => AppResult.Failure<TResponseData>(message);

    /// <summary>
    /// Creates handler result.
    /// </summary>
    /// <typeparam name="TCommand">Type of the query request.</typeparam>
    /// <typeparam name="TResponseData">Type of the response data model.</typeparam>
    /// <param name="handler">Handler instance.</param>
    /// <param name="message">message.</param>
    /// <returns>Created result.</returns>
    public static IAppResult<TResponseData> NotFound<TCommand, TResponseData>(this ICommandHandler<TCommand, IAppResult<TResponseData>> handler, string message)
        where TCommand : ICommand
        => AppResult.NotFound<TResponseData>(message);

    /// <summary>
    /// Creates handler result.
    /// </summary>
    /// <typeparam name="TCommand">Type of the query request.</typeparam>
    /// <typeparam name="TResponseData">Type of the response data model.</typeparam>
    /// <param name="handler">Handler instance.</param>
    /// <param name="message">message.</param>
    /// <returns>Created result.</returns>
    public static IAppResult<TResponseData> Unauthorized<TCommand, TResponseData>(this ICommandHandler<TCommand, IAppResult<TResponseData>> handler, string message)
        where TCommand : ICommand
        => AppResult.Unauthorized<TResponseData>(message);

    /// <summary>
    /// Creates handler result.
    /// </summary>
    /// <typeparam name="TCommand">Type of the query request.</typeparam>
    /// <typeparam name="TResponseData">Type of the response data model.</typeparam>
    /// <param name="handler">Handler instance.</param>
    /// <param name="message">message.</param>
    /// <returns>Created result.</returns>
    public static IAppResult<TResponseData> ValidationFailed<TCommand, TResponseData>(this ICommandHandler<TCommand, IAppResult<TResponseData>> handler, string message)
        where TCommand : ICommand
        => AppResult.ValidationFailed<TResponseData>(message);

    /// <summary>
    /// Creates successful handler result.
    /// </summary>
    /// <typeparam name="TCommand">Type of the query request.</typeparam>
    /// <param name="handler">Handler instance.</param>
    /// <param name="message">Success message.</param>
    /// <returns>Created success result.</returns>
    public static IAppResult Success<TCommand>(this ICommandHandler<TCommand, IAppResult> handler, string message)
        where TCommand : ICommand
        => AppResult.Success(message);

    /// <summary>
    /// Creates handler result.
    /// </summary>
    /// <typeparam name="TCommand">Type of the query request.</typeparam>
    /// <param name="handler">Handler instance.</param>
    /// <param name="message">message.</param>
    /// <returns>Created result.</returns>
    public static IAppResult Failure<TCommand>(this ICommandHandler<TCommand, IAppResult> handler, string message)
        where TCommand : ICommand
        => AppResult.Failure(message);

    /// <summary>
    /// Creates handler result.
    /// </summary>
    /// <typeparam name="TCommand">Type of the query request.</typeparam>
    /// <param name="handler">Handler instance.</param>
    /// <param name="message">message.</param>
    /// <returns>Created result.</returns>
    public static IAppResult NotFound<TCommand>(this ICommandHandler<TCommand, IAppResult> handler, string message)
        where TCommand : ICommand
        => AppResult.NotFound(message);

    /// <summary>
    /// Creates handler result.
    /// </summary>
    /// <typeparam name="TCommand">Type of the query request.</typeparam>
    /// <param name="handler">Handler instance.</param>
    /// <param name="message">message.</param>
    /// <returns>Created result.</returns>
    public static IAppResult Unauthorized<TCommand>(this ICommandHandler<TCommand, IAppResult> handler, string message)
        where TCommand : ICommand
        => AppResult.Unauthorized(message);

    /// <summary>
    /// Creates handler result.
    /// </summary>
    /// <typeparam name="TCommand">Type of the query request.</typeparam>
    /// <param name="handler">Handler instance.</param>
    /// <param name="message">message.</param>
    /// <returns>Created result.</returns>
    public static IAppResult ValidationFailed<TCommand>(this ICommandHandler<TCommand, IAppResult> handler, string message)
        where TCommand : ICommand
        => AppResult.ValidationFailed(message);
}
