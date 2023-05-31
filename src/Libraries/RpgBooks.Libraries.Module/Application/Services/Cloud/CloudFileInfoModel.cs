namespace RpgBooks.Libraries.Module.Application.Services.Cloud;

/// <summary>
/// Cloud file information model.
/// </summary>
public sealed class CloudFileInfoModel
{
    /// <summary>
    /// Gets a value indicating whether if the upload of the file was successful.
    /// </summary>
    public bool UploadSuccess { get; init; }

    /// <summary>
    /// Gets the container name in which the file/blob is stored.
    /// </summary>
    public string ContainerName { get; init; } = default!;

    /// <summary>
    /// Gets the unique cloud file/blob name.
    /// </summary>
    public string CloudFileName { get; init; } = default!;

    /// <summary>
    /// Gets file/blob object version id.
    /// </summary>
    public string VersionId { get; init; } = default!;

    /// <summary>
    /// Gets file/blob object ETag.
    /// </summary>
    public string Tag { get; init; } = default!;

    /// <summary>
    /// Gets the last modified date.
    /// </summary>
    public DateTimeOffset DateModified { get; init; }
}
