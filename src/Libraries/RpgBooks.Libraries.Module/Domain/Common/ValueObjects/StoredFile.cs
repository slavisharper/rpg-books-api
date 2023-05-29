namespace RpgBooks.Libraries.Module.Domain.Common.ValueObjects;

using RpgBooks.Libraries.Module.Domain.Entities;

using System.File;

/// <summary>
/// Data used for accessing file uploaded to a cloud service.
/// </summary>
public sealed record StoredFile : ValueObject<StoredFile>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StoredFile"/> class.
    /// </summary>
    /// <param name="fileName">Original file name.</param>
    /// <param name="accessToken">Document access token.</param>
    public StoredFile(string fileName, string accessToken)
    {
        FileName = fileName;
        AccessToken = accessToken;
    }

    /// <summary>
    /// Gets file access token used for retrieving it.
    /// </summary>
    public string AccessToken { get; init; } = default!;

    /// <summary>
    /// Gets originally uploaded file name.
    /// </summary>
    public string FileName { get; init; } = default!;

    /// <summary>
    /// Gets file extension.
    /// </summary>
    public string Extension => Path.GetExtension(FileName);

    /// <summary>
    /// Gets file mime type.
    /// </summary>
    public string MimeType => MimeTypeHelpers.GetMimeType(FileName);

    /// <inheritdoc />
    public override StoredFile Copy()
        => new(FileName, AccessToken);
}
