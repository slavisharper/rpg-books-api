namespace RpgBooks.Libraries.Module.Domain.Entities;

using RpgBooks.Libraries.Module.Domain.Entities.Abstractions;


/// <summary>
/// Base entity class.
/// </summary>
/// <typeparam name="T">type of base class id.</typeparam>
public abstract class BaseEntity<T> : BaseEntity, IEntity<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseEntity{T}"/> class.
    /// </summary>
    protected BaseEntity()
    {
        Id = default!;
    }

    /// <summary>
    /// Gets or sets id of the entity.
    /// </summary>
    public virtual T Id { get; protected set; }

    /// <inheritdoc/>
    public override bool IsTransient()
    {
        if (Id is null)
        {
            return true;
        }

        return Id.Equals(default(T));
    }

    /// <inheritdoc />
    public override string GetIdentifier()
    {
        return Id?.ToString() ?? default!;
    }

    /// <inheritdoc />
    public override Type GetIdentifierType()
    {
        return typeof(T);
    }
}
