namespace RpgBooks.Libraries.Module.Domain.Entities;

using RpgBooks.Libraries.Module.Domain.Entities.Abstractions;
using RpgBooks.Libraries.Module.Domain.Events;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Base domain entity class.
/// </summary>
public abstract class BaseEntity : IBaseEntity
{
    private ICollection<IDomainEvent> events;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseEntity{T}"/> class.
    /// </summary>
    protected BaseEntity()
    {
        events = new List<IDomainEvent>();
    }

    /// <inheritdoc/>
    public IReadOnlyCollection<IDomainEvent> Events => events.ToArray();

    /// <summary>
    /// Equality operator. Entities are compare only by their identifiers.
    /// <para>If they are transient (not persisted yet), they are not equal.</para>
    /// </summary>
    /// <param name="left">Compared entity.</param>
    /// <param name="right">Comparer entity.</param>
    /// <returns>True if two entities are equal. </returns>
    public static bool operator ==(BaseEntity? left, BaseEntity? right)
        => EqualityComparer<BaseEntity>.Default.Equals(left, right);

    /// <summary>
    /// Not equality operator. Entities are compare only by their identifiers.
    /// <para>If they are transient (not persisted yet), they are not equal.</para>
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(BaseEntity? left, BaseEntity? right)
        => !(left == right);

    /// <inheritdoc/>
    public void AddEvent(IDomainEvent domainEvent)
        => events.Add(domainEvent);

    /// <inheritdoc/>
    public void ClearEvents()
    {
        var list = new List<IDomainEvent>();
        foreach (var evnt in events)
        {
            if (!evnt.Handled && evnt.Published)
            {
                // TODO: event was not handled. Save it to DB or log it for further investigation.
                list.Add(evnt);
            }
        }

        events = list;
    }

    /// <inheritdoc/>
    public abstract bool IsTransient();

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        var compareTo = obj as BaseEntity;
        if (compareTo is null)
        {
            return false;
        }

        return this.GetIdentifier().Equals(compareTo.GetIdentifier());
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        int hashCode = base.GetHashCode();
        if (!IsTransient())
        {
            hashCode = HashCode.Combine(hashCode, this.GetIdentifier().GetHashCode());
        }

        return hashCode;
    }

    /// <inheritdoc />
    public abstract string GetIdentifier();

    /// <inheritdoc />
    public abstract Type GetIdentifierType();
}
