namespace RpgBooks.Libraries.Templates.Exceptions;

/// <summary>
/// Template render exception thrown when fluid template is rendered/compiled.
/// </summary>
public sealed class TemplateRenderException : Exception
{
    private const string DefaultMessage = "Error while rendering template!";

    /// <summary>
    /// Initializes a new instance of the <see cref="TemplateRenderException"/> class.
    /// </summary>
    public TemplateRenderException()
        : base(DefaultMessage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TemplateRenderException"/> class.
    /// </summary>
    /// <param name="message">Specifies an exception message.</param>
    public TemplateRenderException(string message)
        : base(message)
    {
    }
}
