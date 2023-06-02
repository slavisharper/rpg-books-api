namespace RpgBooks.Libraries.Module.Application.Commands;

using RpgBooks.Libraries.Module.Application.Results;

public sealed class CommandHandlerResult<TCommandResult>
{
    public CommandHandlerResult(IEnumerable<AppResultError> errors)
    {
        this.ValidationErrors = errors;
    }

    public CommandHandlerResult(TCommandResult result)
    {
        this.Result = result;
    }

    public bool HasValidationErrors => this.ValidationErrors != null && this.ValidationErrors.Any();

    public TCommandResult? Result { get; }

    public IEnumerable<AppResultError>? ValidationErrors { get; }
}