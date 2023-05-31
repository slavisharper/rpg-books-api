namespace RpgBooks.Libraries.Module.Application.Exceptions;

using RpgBooks.Libraries.Module.Application.Resources;

/// <summary>
/// Represents error that is indicating concurrency problem.
/// </summary>
public sealed class ApplicationConcurrencyException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationConcurrencyException"/> class.
    /// </summary>
    /// <param name="message">Error message.</param>
    public ApplicationConcurrencyException(string? message)
        : base(message ?? Messages.ConcurrencyFailure)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationConcurrencyException"/> class.
    /// </summary>
    /// <param name="concurrencyException">Concurrency exception.</param>
    public ApplicationConcurrencyException(Exception concurrencyException)
        : base(concurrencyException.Message, concurrencyException)
    {
    }
}
