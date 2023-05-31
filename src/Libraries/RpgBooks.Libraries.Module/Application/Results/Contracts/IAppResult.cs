namespace RpgBooks.Libraries.Module.Application.Results.Contracts;

/// <summary>
/// Represents a base CQRS response.
/// </summary>
public interface IAppResult
{
    /// <summary>
    /// Gets the result message.
    /// </summary>
    string Message { get; }

    /// <summary>
    /// Gets a value indicating whether if the request is handled successfully.
    /// </summary>
    bool IsSuccess { get; }

    /// <summary>
    /// Gets a failure reason type that adds better context for a failed request.
    /// </summary>
    FailureReason FailureReason { get; }

    /// <summary>
    /// Gets all errors.
    /// </summary>
    IReadOnlyCollection<IError> Errors { get; }

    /// <summary>
    /// Adds errors to the current result.
    /// </summary>
    /// <param name="errors">Errors list.</param>
    void AddErrors(IEnumerable<IError> errors);
}

/// <summary>
/// Represents CQRS response that holds data.
/// </summary>
/// <typeparam name="TResponseData">Type of the response data model.</typeparam>
public interface IAppResult<TResponseData> : IAppResult
{
    /// <summary>
    /// Gets the response data.
    /// </summary>
    TResponseData Data { get; }
}