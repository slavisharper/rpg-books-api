namespace RpgBooks.Libraries.Module.Presentation.Endpoints.Models;

using RpgBooks.Libraries.Module.Application.Results.Contracts;

/// <summary>
/// Result model for error
/// <para>This is the model returned from the API when error occurs.</para>
/// </summary>
public sealed record ErrorResultModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorResultModel"/> class.
    /// </summary>
    /// <param name="message">Result message.</param>
    /// <param name="errors">Result errors.</param>
    public ErrorResultModel(string message, IEnumerable<IError> errors)
    {
        this.Message = message;
        this.Errors = errors;
    }

    /// <summary>
    /// Gets result error message.
    /// </summary>
    public string Message { get; init; }

    /// <summary>
    /// Gets result data.
    /// </summary>
    public IEnumerable<IError> Errors { get; init; }
}
