namespace RpgBooks.Libraries.Module.Presentation.Endpoints.Abstractions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

/// <summary>
/// Interface for registering minimal API endpoints.
/// </summary>
public interface IApiEndpoint
{
    /// <summary>
    /// Gets endpoints access URL.
    /// </summary>
    string Path { get; }

    /// <summary>
    /// Gets endpoint name used for accessing it from <see cref="LinkGenerator"/>.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets endpoint tag. Used for grouping endpoints by tag names.
    /// </summary>
    string Tag { get; }

    /// <summary>
    /// Gets endpoint HTTP access type.
    /// </summary>
    EndpointTypes Type { get; }

    /// <summary>
    /// Gets the delegate that will be executed when endpoint is accessed.
    /// </summary>
    Delegate Handler { get; }

    /// <summary>
    /// Gets the endpoint response type.
    /// </summary>
    Type ResponseType { get; }

    /// <summary>
    /// Gets the full path of the endpoint combined with the prefix.
    /// </summary>
    /// <param name="prefix">Endpoint prefix.</param>
    /// <returns>Combined endpoint path.</returns>
    string GetApiPath(string prefix = "");
}
