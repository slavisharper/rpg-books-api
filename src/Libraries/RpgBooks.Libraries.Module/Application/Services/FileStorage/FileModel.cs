namespace RpgBooks.Libraries.Module.Application.Services.FileStorage;

/// <summary>
/// File model used for file retrieval as response data model.
/// </summary>
public sealed record FileModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FileModel"/> class.
    /// </summary>
    public FileModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FileModel"/> class.
    /// </summary>
    /// <param name="fileName">File name.</param>
    /// <param name="bytes">File content.</param>
    /// <param name="contentType">File content type.</param>
    public FileModel(string fileName, byte[] bytes, string contentType)
    {
        this.ContentType = contentType;
    }

    /// <summary>
    /// Gets the full name of the file.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets the content type of the file.
    /// </summary>
    public string ContentType { get; set; } = default!;

    /// <summary>
    /// Gets the file data.
    /// </summary>
    public byte[] Data { get; set; } = default!;
}
