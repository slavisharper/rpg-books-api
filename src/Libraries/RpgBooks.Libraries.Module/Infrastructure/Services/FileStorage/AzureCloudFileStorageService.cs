namespace RpgBooks.Libraries.Module.Infrastructure.Services.FileStorage;

using Cysharp.Text;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

using RpgBooks.Libraries.Module.Infrastructure.Resources;
using RpgBooks.Libraries.Module.Application.Results;
using RpgBooks.Libraries.Module.Application.Results.Contracts;
using RpgBooks.Libraries.Module.Application.Services.Cloud;
using RpgBooks.Libraries.Module.Application.Services.FileStorage;
using RpgBooks.Libraries.Module.Application.Settings;
using RpgBooks.Libraries.Module.Domain.Common.ValueObjects;

using System;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Service responsible for storing and managing files to a cloud service.
/// </summary>
public sealed class AzureCloudFileStorageService : ICloudFileStorageService
{
    private readonly IAzureBlobService cloudFileService;
    private readonly ApplicationSettings settings;
    private readonly ApplicationSecrets secrets;

    /// <summary>
    /// Initializes a new instance of the <see cref="AzureCloudFileStorageService"/> class.
    /// </summary>
    /// <param name="cloudFileService">Cloud file service.</param>
    /// <param name="settingsOptions">Application settings.</param>
    /// <param name="secretsOptions">Application secrets.</param>
    public AzureCloudFileStorageService(
        IAzureBlobService cloudFileService,
        IOptions<ApplicationSettings> settingsOptions,
        IOptions<ApplicationSecrets> secretsOptions)
    {
        this.cloudFileService = cloudFileService;
        this.settings = settingsOptions.Value;
        this.secrets = secretsOptions.Value;
    }

    /// <inheritdoc/>
    public async Task<IAppResult<FileModel>> GetFileAsync(string downloadToken, CancellationToken cancellation = default)
    {
        AzureCloudFileDownloadToken token = downloadToken.Decrypt(this.secrets.CloudFileStorageSecret);
        var fileContent = await this.cloudFileService.DownloadAsync(token.ContainerName, token.BlobName, cancellation);

        return fileContent is null
            ? AppResult.NotFound<FileModel>(Messages.CloudFileNotFound)
            : AppResult.Success(Messages.CloudFileFetched, new FileModel(token.FileName, fileContent.ToArray(), token.ContentType));
    }

    /// <inheritdoc/>
    public async Task<IAppResult<CloudFile>> StoreFileAsync(IFormFile fileUpload, CancellationToken cancellation = default)
    {
        string containerName = GetContainerName();
        string blobName = GetNewBlobName(fileUpload.FileName);

        using var fileStream = fileUpload.OpenReadStream();
        var uploadResult = await this.cloudFileService.UploadAsync(
            containerName,
            blobName,
            fileStream,
            cancellation);

        if (uploadResult.UploadSuccess)
        {
            return AppResult.Success(Messages.FileStored, GetStoredFileModel(fileUpload.FileName, blobName, containerName));
        }

        return AppResult.Failure<CloudFile>(Messages.FileStoringFailed);
    }

    /// <inheritdoc/>
    public async Task<IAppResult<CloudFile>> StoreFileAsync(string fileName, byte[] data, CancellationToken cancellation = default)
    {
        string containerName = GetContainerName();
        string blobName = GetNewBlobName(Path.GetExtension(fileName));

        var uploadResult = await this.cloudFileService.UploadAsync(
            containerName,
            blobName,
            data,
            cancellation);

        if (uploadResult.UploadSuccess)
        {
            return AppResult.Success(Messages.FileStored, GetStoredFileModel(fileName, blobName, containerName));
        }

        return AppResult.Failure<CloudFile>(Messages.FileStoringFailed);
    }

    private static string GetNewBlobName(string extension)
        => ZString.Format("{0}.{1}", Ulid.NewUlid(), Path.GetExtension(extension));

    private string GetContainerName()
        => ZString.Format(
            "file-storage-{0}-{1}",
            this.settings.ApplicationName.ToLower(),
            EnvironmentVariables.EnvironmentName.ToLower());

    private CloudFile GetStoredFileModel(string fileName, string blobName, string containerName)
    {
        string token = new AzureCloudFileDownloadToken(containerName, fileName, blobName);
        return new CloudFile(fileName, token.Encrypt(this.secrets.CloudFileStorageSecret));
    }
}
