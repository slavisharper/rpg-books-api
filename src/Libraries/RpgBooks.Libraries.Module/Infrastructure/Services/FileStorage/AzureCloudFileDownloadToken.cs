namespace RpgBooks.Libraries.Module.Infrastructure.Services.FileStorage;

using Cysharp.Text;

using System.File;

internal sealed record AzureCloudFileDownloadToken
{
    private const string TokenPartSeparator = "/";

    public AzureCloudFileDownloadToken(string containerName, string fileName, string blobName)
    {
        this.ContainerName = containerName;
        this.FileName = fileName;
        this.BlobName = blobName;
    }

    public string ContainerName { get; set; }

    /// <summary>
    /// Gets the original file name.
    /// </summary>
    public string FileName { get; init; }

    /// <summary>
    /// Gets the stored blob name
    /// </summary>
    public string BlobName { get; init; }

    /// <summary>
    /// Gets file extension.
    /// </summary>
    public string Extension => Path.GetExtension(FileName);

    /// <summary>
    /// Gets file mime type.
    /// </summary>
    public string ContentType => MimeTypeHelpers.GetMimeType(FileName);

    /// <summary>
    /// Implicitly converts the token to a string.
    /// </summary>
    /// <param name="token">Token value.</param>
    public static implicit operator string(AzureCloudFileDownloadToken token)
        => ZString.Format("{0}{3}{1}{3}{2}", token.ContainerName, token.FileName, token.BlobName, TokenPartSeparator);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    public static implicit operator AzureCloudFileDownloadToken(string token)
    {
        var parts = token.Split(TokenPartSeparator);
        return new AzureCloudFileDownloadToken(parts[0], parts[1], parts[2]);
    }
}
