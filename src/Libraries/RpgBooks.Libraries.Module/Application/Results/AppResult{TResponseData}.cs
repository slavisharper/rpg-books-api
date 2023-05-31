namespace RpgBooks.Libraries.Module.Application.Results;

using RpgBooks.Libraries.Module.Application.Results.Contracts;

/// <summary>
/// Handler result with response data.
/// </summary>
/// <typeparam name="TResponseData">Type of the response data.</typeparam>
public sealed class AppResult<TResponseData> : AppResult, IAppResult<TResponseData>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AppResult{TResponseData}"/> class.
    /// </summary>
    /// <param name="message">Success message.</param>
    /// <param name="responseData">Response data model.</param>
    internal AppResult(string message, TResponseData responseData)
        : base(message)
    {
        this.Data = responseData;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AppResult{TResponseData}"/> class.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="failureReason">Failure reason.</param>
    internal AppResult(string message, FailureReason failureReason)
        : base(message, failureReason)
    {
        this.Data = default!;
    }

    /// <inheritdoc/>
    public TResponseData Data { get; init; }
}