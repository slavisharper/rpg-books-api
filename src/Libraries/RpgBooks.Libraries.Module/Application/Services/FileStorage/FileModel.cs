namespace RpgBooks.Libraries.Module.Application.Services.FileStorage;

/// <summary>
/// File model used for file retrieval as response data model.
/// </summary>
public sealed record FileModel
{
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
