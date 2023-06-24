namespace RpgBooks.Libraries.Module.Infrastructure.Services.FileStorage;

using Microsoft.AspNetCore.Http;

using RpgBooks.Libraries.Module.Application.Results.Contracts;
using RpgBooks.Libraries.Module.Application.Services.FileStorage;
using RpgBooks.Libraries.Module.Domain.Common.ValueObjects;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Service responsible for storing and managing files to the local file system.
/// </summary>
public sealed class LocalFileStorageService : ILocalFileStorageService
{
    /// <inheritdoc/>
    public Task<IAppResult<FileModel>> GetFileAsync(string downloadToken, CancellationToken cancellation = default)
        => throw new NotImplementedException();
    
    /// <inheritdoc/>
    public Task<IAppResult<CloudFile>> StoreFileAsync(IFormFile fileUpload, CancellationToken cancellation = default)
        => throw new NotImplementedException();
    
    /// <inheritdoc/>
    public Task<IAppResult<CloudFile>> StoreFileAsync(string fileName, byte[] data, CancellationToken cancellation = default)
        => throw new NotImplementedException();
}
