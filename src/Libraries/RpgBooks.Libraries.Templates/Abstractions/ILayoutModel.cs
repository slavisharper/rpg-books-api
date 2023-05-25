namespace RpgBooks.Libraries.Templates.Abstractions;

/// <summary>
/// Layout interface that will be used for rendering the whole template.
/// </summary>
public interface ILayoutModel
{
    /// <summary>
    /// Gets or sets specific body content. This contains the rendered template.
    /// </summary>
    string Content { get; set; }
}
