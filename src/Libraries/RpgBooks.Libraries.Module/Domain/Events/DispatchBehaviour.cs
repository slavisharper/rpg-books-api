namespace RpgBooks.Libraries.Module.Domain.Events;

/// <summary>
/// Event dispatcher behavior.
/// </summary>
public enum DispatchBehaviour
{
    /// <summary>
    /// Calls every domain event handler and waits its execution to finish in oder to run the next handler.
    /// </summary>
    InSequence = 1,

    /// <summary>
    /// Runs all handlers in parallel and waits their execution to finish.
    /// </summary>
    InParallel = 2,
}
