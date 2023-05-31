namespace RpgBooks.Libraries.Module.Infrastructure.Services.FileStorage;

using Cysharp.Text;

using System.File;

internal sealed class LocalFileDownloadToken
{
    private const string TokenPartSeparator = "/";

    public LocalFileDownloadToken(string fileName, string storedFileName)
    {
        this.FileName = fileName;
        this.StoredFileName = storedFileName;
    }

    /// <summary>
    /// Gets the original file name.
    /// </summary>
    public string FileName { get; init; }

    /// <summary>
    /// Gets the stored blob name
    /// </summary>
    public string StoredFileName { get; init; }

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
    public static implicit operator string(LocalFileDownloadToken token)
        => ZString.Format("{0}{2}{1}", token.FileName, token.StoredFileName, TokenPartSeparator);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    public static implicit operator LocalFileDownloadToken(string token)
    {
        var parts = token.Split(TokenPartSeparator);
        return new LocalFileDownloadToken(parts[0], parts[1]);
    }
}
