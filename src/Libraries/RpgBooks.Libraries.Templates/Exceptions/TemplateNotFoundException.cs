namespace RpgBooks.Libraries.Templates.Exceptions;

/// <summary>
/// Template not found exception.
/// </summary>
public sealed class TemplateNotFoundException : Exception
{
    private const string DefaultMessage = "Template not found!";

    /// <summary>
    /// Initializes a new instance of the <see cref="TemplateRenderException"/> class.
    /// </summary>
    public TemplateNotFoundException()
        : base(DefaultMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TemplateRenderException"/> class.
    /// </summary>
    /// <param name="message">Specifies an exception message.</param>
    public TemplateNotFoundException(string message)
        : base(message)
    {
    }
}
