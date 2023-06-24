namespace RpgBooks.Libraries.Module.Application.Services.FileStorage;

using Microsoft.AspNetCore.Http;

using RpgBooks.Libraries.Module.Application.Results.Contracts;
using RpgBooks.Libraries.Module.Domain.Common.ValueObjects;

/// <summary>
/// Service responsible for storing and managing files to a cloud service.
/// </summary>
public interface IFileStorageService
{
    /// <summary>
    /// Stores given file upload to a file storage service.
    /// </summary>
    /// <param name="fileUpload">Uploaded file.</param>
    /// <param name="cancellation">Cancellation token</param>
    /// <returns></returns>
    Task<IAppResult<CloudFile>> StoreFileAsync(IFormFile fileUpload, CancellationToken cancellation = default);

    /// <summary>
    /// Store given file to a cloud service with personal only access.
    /// This means that this file will be accepted only from the current authenticated user.
    /// </summary>
    /// <param name="fileName">File name with file extension.</param>
    /// <param name="data">File content as byte array.</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>Cloud file meta data.</returns>
    Task<IAppResult<CloudFile>> StoreFileAsync(string fileName, byte[] data, CancellationToken cancellation = default);

    /// <summary>
    /// Retrieve file content from a cloud service.
    /// </summary>
    /// <param name="downloadToken">Encrypted file download access token.</param>
    /// <param name="cancellation">Cancellation token.</param>
    /// <returns>File content as byte array.</returns>
    Task<IAppResult<FileModel>> GetFileAsync(string downloadToken, CancellationToken cancellation = default);
}
