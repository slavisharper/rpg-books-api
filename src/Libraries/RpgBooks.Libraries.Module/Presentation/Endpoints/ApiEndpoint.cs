namespace RpgBooks.Libraries.Module.Presentation.Endpoints;

using Cysharp.Text;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using RpgBooks.Libraries.Module.Presentation.Endpoints.Abstractions;
using RpgBooks.Libraries.Module.Presentation.Endpoints.Extensions;
using RpgBooks.Libraries.Module.Presentation.Endpoints.Models;

/// <summary>
/// Base minimal API endpoint configuration class.
/// </summary>
/// <typeparam name="TRequest">Type of the request data.</typeparam>
public abstract class ApiEndpoint<TRequest> : IApiEndpoint
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApiEndpoint{TRequest}"/> class.
    /// </summary>
    /// <param name="path">Path of the endpoint.</param>
    /// <param name="name">Endpoint unique name.</param>
    /// <param name="tag">Endpoint grouping tag.</param>
    /// <param name="type">Endpoint HTTP access type.</param>
    /// <param name="handler">Endpoint handler.</param>
    protected ApiEndpoint(
        string path,
        string name,
        string tag,
        EndpointTypes type,
        Delegate handler)
    {
        this.Path = path;
        this.Name = name;
        this.Tag = tag;
        this.Type = type;
        this.Handler = handler;
    }

    /// <summary>
    /// Gets or sets endpoints access URL.
    /// </summary>
    public string Path { get; protected set; }

    /// <summary>
    /// Gets or sets endpoint name used for accessing it from <see cref="LinkGenerator"/>.
    /// </summary>
    public string Name { get; protected set; }

    /// <summary>
    /// Gets or sets endpoint tag. Used for grouping endpoints by tag names.
    /// </summary>
    public string Tag { get; protected set; }

    /// <summary>
    /// Gets or sets endpoint HTTP access type.
    /// </summary>
    public EndpointTypes Type { get; protected set; }

    /// <summary>
    /// Gets or sets the delegate that will be executed when endpoint is accessed.
    /// </summary>
    public Delegate Handler { get; protected set; }

    /// <inheritdoc/>
    public virtual Type ResponseType => typeof(SuccessResultModel);

    /// <inheritdoc/>
    public string GetApiPath(string prefix = "")
        => string.IsNullOrEmpty(prefix) ? this.Path : ZString.Format("{0}{1}", prefix, this.Path);
}
