namespace RpgBooks.Libraries.Module.Application.Commands;

using FluentValidation;
using FluentValidation.Results;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using RpgBooks.Libraries.Module.Application.Commands.Contracts;
using RpgBooks.Libraries.Module.Application.Results;

using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Command requests dispatcher that calls only the first found command handler.
/// </summary>
public sealed class SingleCommandHandlerDispatcher : ICommandHandlerDispatcher
{
    private readonly IServiceProvider serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="SingleCommandHandlerDispatcher"/> class.
    /// </summary>
    /// <param name="serviceProvider">Service provider.</param>
    public SingleCommandHandlerDispatcher(IServiceProvider serviceProvider)
        => this.serviceProvider = serviceProvider;

    /// <inheritdoc/>
    public async Task<CommandHandlerResult<TCommandResult>> Dispatch<TCommand, TCommandResult>(TCommand command, CancellationToken cancellation)
        where TCommand : ICommand
    {
        var logger = this.serviceProvider.GetRequiredService<ILogger>();
        var failures = await ValidateRequest(command, cancellation);
        var commandType = typeof(TCommand);

        if (failures.Any())
        {
            logger.LogRequestValidationFailed(commandType.Name);
            return new CommandHandlerResult<TCommandResult>(failures);
        }

        var timeStamp = Stopwatch.GetTimestamp();
        var handlerResult = await this.serviceProvider
            .GetRequiredService<ICommandHandler<TCommand, TCommandResult>>()
            .Handle(command, cancellation);

        logger.LogRequestHandlingTime(commandType.Name, Stopwatch.GetElapsedTime(timeStamp).Milliseconds);
        return new CommandHandlerResult<TCommandResult>(handlerResult);
    }

    private async Task<IEnumerable<AppResultError>> ValidateRequest<TCommand>(TCommand command, CancellationToken cancellation) where TCommand : ICommand
    {
        IEnumerable<IValidator<TCommand>> validators = this.serviceProvider.GetServices<IValidator<TCommand>>();
        var context = new ValidationContext<TCommand>(command);

        var errors = new List<ValidationFailure>();
        if (validators.Any())
        {
            foreach (var validator in validators)
            {
                var result = await validator.ValidateAsync(context, cancellation);
                if (result.Errors.Any())
                {
                    errors.AddRange(result.Errors);
                }
            }
        }

        return errors
            .GroupBy(f => f.PropertyName)
            .Select(g => new AppResultError(g.Key, g.Select(f => f.ErrorMessage).ToArray()));
    }
}
