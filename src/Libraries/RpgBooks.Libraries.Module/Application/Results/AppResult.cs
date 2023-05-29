namespace RpgBooks.Libraries.Module.Application.Results;

using RpgBooks.Libraries.Module.Application.Results.Contracts;

using System.Collections.Generic;

/// <summary>
/// Implements <see cref="IAppResult"/> and it is used as return type for the application request handlers.
/// </summary>
public class AppResult : IAppResult
{
    private readonly HashSet<IError> errors;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppResult"/> class.
    /// </summary>
    /// <param name="message">Success message.</param>
    internal AppResult(string message)
    {
        this.Message = message;
        this.IsSuccess = true;
        this.FailureReason = FailureReason.None;
        this.errors = default!;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AppResult"/> class.
    /// </summary>
    /// <param name="message">Failure message.</param>
    /// <param name="failureReason">Failure reason.</param>
    internal AppResult(string message, FailureReason failureReason)
    {
        this.Message = message;
        this.IsSuccess = false;
        this.FailureReason = failureReason;
        this.errors = new();
    }

    /// <inheritdoc/>
    public string Message { get; }

    /// <inheritdoc/>
    public bool IsSuccess { get; }

    /// <inheritdoc/>
    public FailureReason FailureReason { get; }

    /// <inheritdoc/>
    public IReadOnlyCollection<IError> Errors => this.errors;

    /// <summary>
    /// Creates result that represents success.
    /// </summary>
    /// <param name="message">Result success message.</param>
    /// <returns>Created success result.</returns>
    public static AppResult Success(string message)
        => new(message);

    /// <summary>
    /// Creates result that represents success.
    /// </summary>
    /// <typeparam name="TResponseData">Type of the response data.</typeparam>
    /// <param name="message">Success message.</param>
    /// <param name="data">Result data model.</param>
    /// <returns>Created success result.</returns>
    public static AppResult<TResponseData> Success<TResponseData>(string message, TResponseData data)
        => new(message, data);

    /// <summary>
    /// Creates result that represents general failure.
    /// </summary>
    /// <param name="message">Result failure message.</param>
    /// <returns>Created failure result.</returns>
    public static AppResult Failure(string message)
        => new(message, FailureReason.General);

    /// <inheritdoc cref="Failure(string)"/>
    public static AppResult<TResponseData> Failure<TResponseData>(string message)
        => new(message, FailureReason.General);

    /// <summary>
    /// Creates result that represents not found.
    /// </summary>
    /// <param name="message">Result not found message.</param>
    /// <returns>Created not found result.</returns>
    public static AppResult NotFound(string message)
        => new(message, FailureReason.NotFound);

    /// <inheritdoc cref="NotFound(string)"/>
    public static AppResult<TResponseData> NotFound<TResponseData>(string message)
        => new(message, FailureReason.NotFound);

    /// <summary>
    /// Creates result that represents validation failure.
    /// </summary>
    /// <param name="message">Failed validation message.</param>
    /// <returns>Created validation failed result.</returns>
    public static AppResult ValidationFailed(string message)
        => new(message, FailureReason.ValidationFailed);

    /// <inheritdoc cref="ValidationFailed(string)"/>
    public static AppResult<TResponseData> ValidationFailed<TResponseData>(string message)
        => new(message, FailureReason.ValidationFailed);

    /// <summary>
    /// Creates result that unauthorized access failure.
    /// </summary>
    /// <param name="message">Result success message.</param>
    /// <returns>Created unauthorized result.</returns>
    public static AppResult Unauthorized(string message)
        => new(message, FailureReason.Unauthorized);

    /// <inheritdoc cref="Unauthorized(string)"/>
    public static AppResult<TResponseData> Unauthorized<TResponseData>(string message)
        => new(message, FailureReason.Unauthorized);

    /// <summary>
    /// Adds new error to the result.
    /// </summary>
    /// <param name="key">Error key.</param>
    /// <param name="message">Error message.</param>
    /// <returns>Current result instance used for chaining.</returns>
    public AppResult WithError(string key, string message)
    {
        this.errors.Add(new AppResultError(key, message));
        return this;
    }

    /// <summary>
    /// Adds errors to the result.
    /// </summary>
    /// <param name="errors">Validation errors.</param>
    public void AddErrors(IEnumerable<IError> errors)
    {
        foreach (var error in errors)
        {
            this.errors.Add(error);
        }
    }
}
