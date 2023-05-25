namespace RpgBooks.Libraries.Templates.Models;

using RpgBooks.Libraries.Templates.Abstractions;

public class BaseLayoutModel : ILayoutModel
{
    public string Content { get; set; } = default!;
}
