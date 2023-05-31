namespace RpgBooks.Libraries.Module.Infrastructure.Services;

using Microsoft.AspNetCore.Routing;

using RpgBooks.Libraries.Module.Application.Services;

using System.Collections.Concurrent;

/// <summary>
/// Endpoints URL provider.
/// </summary>
public sealed class UrlProvider : IUrlProvider
{
    private static readonly object EmptyArgs = new { };

    private readonly LinkGenerator linkGenerator;
    private readonly ConcurrentDictionary<string, string> requestToEndpointNames;

    /// <summary>
    /// Initializes a new instance of the <see cref="UrlProvider"/> class.
    /// </summary>
    /// <param name="linkGenerator">Link generator.</param>
    public UrlProvider(LinkGenerator linkGenerator)
    {
        this.linkGenerator = linkGenerator;
        this.requestToEndpointNames = new ConcurrentDictionary<string, string>();
    }

    /// <inheritdoc/>
    public string? GetRequestUrl<TRequest>()
        => this.GetRequestUrl(typeof(TRequest), EmptyArgs);

    /// <inheritdoc/>
    public string? GetRequestUrl<TRequest>(TRequest args)
        => this.GetRequestUrl(typeof(TRequest), args);

    /// <inheritdoc/>
    public string? GetRequestUrl(Type requestType)
        => this.GetRequestUrl(requestType, EmptyArgs);

    /// <inheritdoc/>
    public string? GetRequestUrl(Type requestType, object? args)
    {
        string requestName = requestType.FullName!;
        this.requestToEndpointNames.TryGetValue(requestName, out string? endpointName);
        if (endpointName is null)
        {
            return null;
        }

        return this.linkGenerator.GetPathByName(endpointName, values: args);
    }

    /// <inheritdoc/>
    public void AddEndpointRequestPair(string endpointName, Type requestType)
    {
        this.requestToEndpointNames.TryAdd(requestType.FullName!, endpointName);
    }
}
