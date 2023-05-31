namespace RpgBooks.Libraries.Templates.Models;

using RpgBooks.Libraries.Templates.Abstractions;

/// <summary>
/// Base model for layout templates.
/// </summary>
public class BaseLayoutModel : ILayoutModel
{
    /// <summary>
    /// Gets or sets the content of the layout template.
    /// This is the actual template content.
    /// </summary>
    public string Content { get; set; } = default!;
}
