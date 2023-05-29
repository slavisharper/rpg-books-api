namespace RpgBooks.Libraries.Templates.Abstractions;

/// <summary>
/// Template info interface.
/// </summary>
public interface ITemplateInfo
{
    /// <summary>
    /// Name of the template used as identifier.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Content of the template that will be rendered.
    /// </summary>
    string? Content { get; }
}
