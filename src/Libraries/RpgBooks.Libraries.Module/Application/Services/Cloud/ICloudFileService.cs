namespace RpgBooks.Libraries.Module.Application.Services.Cloud;

using System.Threading.Tasks;

/// <summary>
/// Files service responsible for storing and retrieving files from cloud storage.
/// </summary>
public interface ICloudFileService
{
    /// <summary>
    /// Uploads blob file to a cloud service.
    /// </summary>
    /// <param name="containerName">Container name in which the blob is stored.</param>
    /// <param name="blobName">Unique Blob file name.</param>
    /// <param name="data">Binary data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Blob meta information.</returns>
    Task<CloudFileInfoModel> UploadAsync(string containerName, string blobName, BinaryData data, CancellationToken cancellationToken = default);

    /// <summary>
    /// Uploads blob file to a cloud service.
    /// </summary>
    /// <param name="containerName">Container name in which the blob is stored.</param>
    /// <param name="blobName">Unique Blob file name.</param>
    /// <param name="data">Byte array data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Blob meta information.</returns>
    Task<CloudFileInfoModel> UploadAsync(string containerName, string blobName, byte[] data, CancellationToken cancellationToken = default);

    /// <summary>
    /// Uploads blob file to a cloud service.
    /// </summary>
    /// <param name="containerName">Container name in which the blob is stored.</param>
    /// <param name="blobName">Unique Blob file name.</param>
    /// <param name="dataStream">File data stream.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Blob meta information.</returns>
    Task<CloudFileInfoModel> UploadAsync(string containerName, string blobName, Stream dataStream, CancellationToken cancellationToken = default);

    /// <summary>
    /// Download blob file content.
    /// </summary>
    /// <param name="containerName">Container name in which the blob is stored.</param>
    /// <param name="blobName">Unique Blob file name.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Blobs content as binary data.</returns>
    Task<BinaryData> DownloadAsync(string containerName, string blobName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete given blob.
    /// </summary>
    /// <param name="containerName">Container name in which the blob is stored.</param>
    /// <param name="blobName">Unique Blob file name.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Task indicating whether the deletion is successful.</returns>
    Task<bool> DeleteAsync(string containerName, string blobName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete container instance from the cloud service.
    /// </summary>
    /// <param name="containerName">Unique container name.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Task indicating whether the deletion is successful.</returns>
    Task<bool> DeleteContainerAsync(string containerName, CancellationToken cancellationToken = default);
}
