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
    /// Register current endpoint to the application builder.
    /// </summary>
    /// <param name="app">Application builder instance.</param>
    /// <returns>Registered endpoint builder.</returns>
    RouteHandlerBuilder Register(WebApplication app);
}
