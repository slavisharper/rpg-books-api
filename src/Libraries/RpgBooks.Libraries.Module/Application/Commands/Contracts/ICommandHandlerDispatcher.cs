namespace RpgBooks.Libraries.Module.Application.Commands.Contracts;

using System.Threading.Tasks;

/// <summary>
/// It defines a command handler dispatcher that is responsible for executing command handlers and getting their result.
/// </summary>
public interface ICommandHandlerDispatcher
{
    /// <summary>
    /// Fins and executes command handler, that can handle given input command request.
    /// </summary>
    /// <typeparam name="TCommand">Represents the type of the command request.</typeparam>
    /// <typeparam name="TCommandResult">Represents the response type that is expected form the handler.</typeparam>
    /// <param name="command">Command request model.</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>Async task that has the handler response.</returns>
    Task<TCommandResult> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellation)
        where TCommand : ICommand;
}
