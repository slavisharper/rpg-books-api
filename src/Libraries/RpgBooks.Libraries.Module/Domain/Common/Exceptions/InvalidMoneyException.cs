namespace RpgBooks.Libraries.Module.Domain.Common.Exceptions;

using RpgBooks.Libraries.Module.Domain.Exceptions;

/// <summary>
/// Represents invalid Money value.
/// </summary>
public class InvalidMoneyException : DomainValidationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidMoneyException"/> class.
    /// </summary>
    public InvalidMoneyException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidMoneyException"/> class.
    /// </summary>
    /// <param name="message">Specifies an exception message.</param>
    public InvalidMoneyException(string message)
        : base(message)
    {
    }
}
