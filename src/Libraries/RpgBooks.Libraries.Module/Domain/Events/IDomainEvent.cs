namespace RpgBooks.Libraries.Module.Domain.Events;

/// <summary>
/// Represents a Domain-level event - a simple POCO class, modeling an occurrence in the domain.
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    /// Gets a value indicating whether the event has been published for processing.
    /// </summary>
    bool Published { get; }

    /// <summary>
    /// Gets a value indicating whether the event has been processed.
    /// </summary>
    bool Handled { get; }

    /// <summary>
    /// Gets a value indicating whether there is problem with the processing of the given event.
    /// </summary>
    bool Poisoned { get; }

    /// <summary>
    /// Gets the instant in time when the event has been raised.
    /// </summary>
    DateTimeOffset OccuredOn { get; }

    /// <summary>
    /// Marks the current event as published for processing.
    /// </summary>
    void MarkAsPublished();

    /// <summary>
    /// Marks the current event as processed.
    /// </summary>
    void MarkAsHandled();
}
