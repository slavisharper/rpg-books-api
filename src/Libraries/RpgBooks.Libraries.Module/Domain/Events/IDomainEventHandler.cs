namespace RpgBooks.Libraries.Module.Domain.Events;

/// <summary>
/// Represents a handler for a Domain-level event.
/// </summary>
/// <typeparam name="TEvent">Event type.</typeparam>
public interface IDomainEventHandler<TEvent>
    where TEvent : IDomainEvent
{
    /// <summary>
    /// Handles the domain event using the local <see cref="IDomainEventHandler{TEvent}"/>.
    /// </summary>
    /// <param name="evnt">Event instance / Event "args".</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the Handle operation.</returns>
    Task HandleEvent(TEvent evnt, CancellationToken cancellationToken = default);
}
