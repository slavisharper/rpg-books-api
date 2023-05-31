namespace RpgBooks.Libraries.Module.Domain.Events;

/// <summary>
/// Represents a domain-level event dispatcher.
/// </summary>
public interface IDomainEventDispatcher
{
    /// <summary>
    /// Dispatches the provided domain-level event.
    /// </summary>
    /// <param name="domainEvent">Event instance.</param>
    /// <param name="dispatchBehaviour">Defines how the events will be dispatched.</param>
    /// <returns>A task representing the dispatch operation.</returns>
    Task Dispatch<TEvent>(TEvent domainEvent, DispatchBehaviour dispatchBehaviour = DispatchBehaviour.InSequence)
        where TEvent : IDomainEvent;
}
