namespace RpgBooks.Libraries.Module.Domain.Events;

/// <summary>
/// General event published when event handler fails.
/// </summary>
public sealed record UnhandledDomainExceptionEvent : BaseDomainEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnhandledDomainExceptionEvent"/> class.
    /// </summary>
    /// <param name="eventName">Event name.</param>
    /// <param name="eventDataJson">Event data in JSON format.</param>
    /// <param name="ex">Event handler exception.</param>
    public UnhandledDomainExceptionEvent(string eventName, string eventDataJson, Exception ex)
    {
        EventName = eventName;
        Exception = ex;
        EventDataJson = eventDataJson;
    }

    /// <summary>
    /// Gets the event name.
    /// </summary>
    public string EventName { get; init; } = default!;

    /// <summary>
    /// Gets the event data in JSON format.
    /// </summary>
    public string EventDataJson { get; init; } = default!;

    /// <summary>
    /// Gets the occurred exception.
    /// </summary>
    public Exception Exception { get; init; } = default!;
}
