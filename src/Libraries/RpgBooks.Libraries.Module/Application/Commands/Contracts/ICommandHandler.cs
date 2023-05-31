namespace RpgBooks.Libraries.Module.Application.Commands.Contracts;

/// <summary>
/// It defines the contract for a class that can handle a specific command and return a result.
/// </summary>
/// <typeparam name="TCommand">Represents the type of the command that the handler can handle.</typeparam>
/// <typeparam name="TCommandResult">Represents the type of the response that is returned from the handler.</typeparam>
public interface ICommandHandler<in TCommand, TCommandResult>
    where TCommand : ICommand
{
    /// <summary>
    /// This method is responsible for executing the logic to handle the command and return the result.
    /// </summary>
    /// <param name="command">Input command request that will be handled.</param>
    /// <param name="context">Command handler context.</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>Task containing the handler response.</returns>
    Task<TCommandResult> Handle(TCommand command, ICommandHandlerContext context, CancellationToken cancellation);
}
