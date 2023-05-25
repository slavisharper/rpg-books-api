namespace RpgBooks.Libraries.Templates.Exceptions;

/// <summary>
/// Exception thrown when Fluid.Parse does not work properly.
/// </summary>
public sealed class TemplateParseException : Exception
{
    private const string DefaultMessage = "Error while parsing rendered/compiled template!";

    /// <summary>
    /// Initializes a new instance of the <see cref="TemplateParseException"/> class.
    /// </summary>
    public TemplateParseException()
        : base(DefaultMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TemplateParseException"/> class.
    /// </summary>
    /// <param name="message">Specifies an exception message.</param>
    public TemplateParseException(string message)
        : base(message)
    {
    }
}
