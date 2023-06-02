namespace RpgBooks.Libraries.Module.Application.Commands;

using RpgBooks.Libraries.Module.Application.Results;

/// <summary>
/// Command handler result that can be used to return data or validation errors.
/// </summary>
/// <typeparam name="TCommandResult"></typeparam>
public sealed class CommandHandlerResult<TCommandResult>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommandHandlerResult{TCommandResult}"/> class.
    /// </summary>
    /// <param name="errors">Validation errors.</param>
    public CommandHandlerResult(IEnumerable<AppResultError> errors)
    {
        this.ValidationErrors = errors;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandHandlerResult{TCommandResult}"/> class.
    /// </summary>
    /// <param name="result">Valid response data.</param>
    public CommandHandlerResult(TCommandResult result)
    {
        this.Result = result;
    }

    /// <summary>
    /// Gets a value indicating whether the result has validation errors.
    /// </summary>
    public bool HasValidationErrors => this.ValidationErrors != null && this.ValidationErrors.Any();

    /// <summary>
    /// Gets the result returned from the command handler.
    /// </summary>
    public TCommandResult? Result { get; }

    /// <summary>
    /// Gets the validation errors that were found from the command validator.
    /// </summary>
    public IEnumerable<AppResultError>? ValidationErrors { get; }
}