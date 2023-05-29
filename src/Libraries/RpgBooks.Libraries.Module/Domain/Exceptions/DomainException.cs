namespace RpgBooks.Libraries.Module.Domain.Exceptions;

/// <summary>
/// Represents generic error occurring in the domain layer.
/// </summary>
public abstract class DomainException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainException"/> class.
    /// </summary>
    public DomainException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DomainException"/> class.
    /// </summary>
    /// <param name="message">Specifies an exception message.</param>
    public DomainException(string message)
        : base(message)
    {
    }
}
