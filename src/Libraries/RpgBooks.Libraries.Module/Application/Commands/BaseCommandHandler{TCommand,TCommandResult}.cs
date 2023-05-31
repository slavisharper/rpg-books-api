﻿namespace RpgBooks.Libraries.Module.Application.Commands;

using Polly;

using RpgBooks.Libraries.Module.Application.Commands.Contracts;
using RpgBooks.Libraries.Module.Application.Exceptions;
using RpgBooks.Libraries.Module.Application.Resources;
using RpgBooks.Libraries.Module.Application.Results;
using RpgBooks.Libraries.Module.Application.Results.Contracts;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Base command handler.
/// </summary>
/// <typeparam name="TCommand">Represents the type of the command request.</typeparam>
/// <typeparam name="TCommandResponseData">Represents the type of the data contained in the IResult response.</typeparam>
public abstract class BaseCommandHandler<TCommand, TCommandResponseData> :  ICommandHandler<TCommand, IAppResult<TCommandResponseData>>
    where TCommand : ICommand
{
    /// <inheritdoc/>
    public Task<IAppResult<TCommandResponseData>> Handle(TCommand command, ICommandHandlerContext context, CancellationToken cancellation)
    {
        if (!context.IsValid)
        {
            IAppResult<TCommandResponseData> failedResult =
                AppResult.ValidationFailed<TCommandResponseData>(Messages.ValidationFailure);
            failedResult.AddErrors(context.Failures);
            return Task.FromResult(failedResult);
        }

        return Policy
            .Handle<ApplicationConcurrencyException>()
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (exception, timeSpan, retryCount, context) =>
                {
                    // this.logger.LogError("Concurrent error '{ErrorMessage}'. Retry calling handler {RetryCount}.", exception.Message, retryCount);
                })
            .ExecuteAsync((task) => this.HandleCommand(command, cancellation), cancellation);
    }

    /// <summary>
    /// Specific handle implementation called after query handle.
    /// </summary>
    /// <param name="request">Input command.</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>Completed task.</returns>
    public abstract Task<IAppResult<TCommandResponseData>> HandleCommand(TCommand request, CancellationToken cancellation);
}
