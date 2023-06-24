namespace RpgBooks.Libraries.Module.Domain.Common.ValueObjects;

using RpgBooks.Libraries.Module.Domain.Entities;

/// <summary>
/// Local file location value object
/// </summary>
public sealed record LocalFile : ValueObject<LocalFile>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LocalFile"/> class.
    /// </summary>
    /// <param name="filePath">Relative file path.</param>
    /// <param name="fileName">File name only.</param>
    public LocalFile(string filePath, string fileName)
    {
        this.FilePath = filePath;
        this.FileName = fileName;
    }

    /// <summary>
    /// Gets the relative file path of the image
    /// </summary>
    public string FilePath { get; init; }

    /// <summary>
    /// Gets the user friendly title of the image
    /// </summary>
    public string FileName { get; init; }

    /// <inheritdoc/>
    public override LocalFile Copy() => new(this.FilePath, this.FileName);
}
