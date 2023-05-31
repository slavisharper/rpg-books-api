namespace RpgBooks.Libraries.Module.Domain.Entities;

/// <summary>
/// Value object base class record
/// </summary>
/// <typeparam name="T">Type if the value object implementation.</typeparam>
public abstract record ValueObject<T>
    where T : ValueObject<T>
{    
    /// <summary>
    /// Duplicates current <typeparamref name="T"/> value object.
    /// </summary>
    /// <returns>Duplicated value object.</returns>
    public abstract T Copy();
}
