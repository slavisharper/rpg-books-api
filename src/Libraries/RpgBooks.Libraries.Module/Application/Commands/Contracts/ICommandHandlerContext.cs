namespace RpgBooks.Libraries.Module.Application.Commands.Contracts;

using RpgBooks.Libraries.Module.Application.Results.Contracts;

/// <summary>
/// Command handler context used for additional information for the command handler.
/// </summary>
public interface ICommandHandlerContext
{
    /// <summary>
    /// Gets a value indicating whether the requested command request is valid.
    /// </summary>
    public bool IsValid { get; }

    /// <summary>
    /// Gets errors list.
    /// </summary>
    public IReadOnlyCollection<IError> Failures { get; }
}
