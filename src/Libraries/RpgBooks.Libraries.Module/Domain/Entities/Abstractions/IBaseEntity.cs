namespace RpgBooks.Libraries.Module.Domain.Entities.Abstractions;

using RpgBooks.Libraries.Module.Domain.Events;

/// <summary>
/// Entity base.
/// </summary>
public interface IBaseEntity
{
    /// <summary>
    /// Gets a read-only collection of the attached domain-level events.
    /// </summary>
    IReadOnlyCollection<IDomainEvent> Events { get; }

    /// <summary>
    /// Attaches a domain-level event to be raised on the current entity.
    /// </summary>
    /// <param name="domainEvent">Domain event instance.</param>
    void AddEvent(IDomainEvent domainEvent);

    /// <summary>
    /// Clears all domain-level event handlers attached to the current entity.
    /// </summary>
    void ClearEvents();

    /// <summary>
    /// Indicates whether the entity is in transient state - e.g. not persistent.
    /// </summary>
    /// <returns>True if object is transient, otherwise false.</returns>
    bool IsTransient();

    /// <summary>
    /// Get entities identifier as string.
    /// </summary>
    /// <returns>Unique entity identifier as string.</returns>
    string? GetIdentifier();

    /// <summary>
    /// Get entities identifier type.
    /// </summary>
    /// <returns>Unique entity identifier type.</returns>
    Type GetIdentifierType();
}
