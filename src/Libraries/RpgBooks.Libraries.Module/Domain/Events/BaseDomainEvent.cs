namespace RpgBooks.Libraries.Module.Domain.Events;

/// <summary>
/// This is the base domain event that should be inherited in most cases.
/// </summary>
public abstract record BaseDomainEvent : IDomainEvent
{
    // Shows how many active processing are taking place.
    // If this is more than 1 means that event was published but not handled.
    private int processingCount = 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseDomainEvent"/> class.
    /// </summary>
    protected BaseDomainEvent()
    {
        OccuredOn = DateTimeProvider.Instance.UtcNow;
    }

    /// <inheritdoc/>
    public bool Published { get; private set; }

    /// <inheritdoc/>
    public bool Handled { get; private set; }

    /// <inheritdoc/>
    public bool Poisoned => processingCount > 1;

    /// <inheritdoc/>
    public DateTimeOffset OccuredOn { get; init; }

    /// <inheritdoc/>
    public void MarkAsPublished()
    {
        processingCount++;
        Published = true;
    }

    /// <inheritdoc/>
    public void MarkAsHandled()
    {
        processingCount--;
        Handled = true;
    }
}
