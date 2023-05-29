namespace RpgBooks.Libraries.Module.Presentation.Endpoints.Filters;

using System.Reflection;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OutputCaching;

/// <summary>
/// Revoke cache filter.
/// </summary>
public sealed class RevokeCacheFilter : IEndpointFilter
{
    private readonly string revokeKey;
    private readonly IOutputCacheStore cacheStore;

    /// <summary>
    /// Initializes a new instance of the <see cref="RevokeCacheFilter"/> class.
    /// </summary>
    /// <param name="cacheStore">Cache store.</param>
    /// <param name="revokeKey">Revoke cache key.</param>
    public RevokeCacheFilter(IOutputCacheStore cacheStore, string revokeKey)
    {
        this.cacheStore = cacheStore;
        this.revokeKey = revokeKey;
    }

    /// <inheritdoc/>
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var result = await next(context);

        await this.cacheStore.EvictByTagAsync(this.revokeKey, CancellationToken.None);

        return result;
    }
}
