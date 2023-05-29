namespace RpgBooks.Libraries.Module.Application.Commands;

using RpgBooks.Libraries.Module.Application.Commands.Contracts;
using RpgBooks.Libraries.Module.Application.Results.Contracts;

using System.Collections.Generic;

/// <summary>
/// Command handler context.
/// </summary>
internal sealed record CommandHandlerContext : ICommandHandlerContext
{
    private readonly IError[] failures;

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandHandlerContext"/> class.
    /// </summary>
    internal CommandHandlerContext(params IError[] failures)
    {
        this.failures = failures;
    }

    /// <summary>
    /// Gets a value indicating whether executed command is valid.
    /// </summary>
    public bool IsValid => !this.failures.Any();

    /// <summary>
    /// Gets errors list.
    /// </summary>
    public IReadOnlyCollection<IError> Failures => this.failures;
}
