namespace RpgBooks.Libraries.Module.Presentation.Endpoints;

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

    /// <inheritdoc />
    public virtual RouteHandlerBuilder Register(WebApplication app)
    {
        var endpointType = this.GetType();

        RouteHandlerBuilder builder = this.GetRouteBuilder(app)
            .ApplyCaching(endpointType)
            .ApplyAuthorization(endpointType)
            .ApplyCacheRevoking(app, endpointType)
            .WithName(this.Name)
            .WithTags(this.Tag);

        if (this.Type == EndpointTypes.Get)
        {
            builder
                .Produces(StatusCodes.Status200OK, typeof(SuccessResultModel))
                .Produces(StatusCodes.Status404NotFound, typeof(ErrorResultModel));
        }
        else if (this.Type == EndpointTypes.Post)
        {
            builder
                .Produces(StatusCodes.Status200OK, typeof(SuccessResultModel))
                .Produces(StatusCodes.Status400BadRequest, typeof(ErrorResultModel));
        }
        else if (this.Type == EndpointTypes.Put)
        {
            builder
                .Produces(StatusCodes.Status200OK, typeof(SuccessResultModel))
                .Produces(StatusCodes.Status404NotFound, typeof(ErrorResultModel))
                .Produces(StatusCodes.Status400BadRequest, typeof(ErrorResultModel));
        }
        else if (this.Type == EndpointTypes.Delete)
        {
            builder
                .Produces(StatusCodes.Status200OK, typeof(SuccessResultModel))
                .Produces(StatusCodes.Status404NotFound, typeof(ErrorResultModel))
                .Produces(StatusCodes.Status400BadRequest, typeof(ErrorResultModel));
        }

        return builder;
    }

    /// <summary>
    /// Gets the route builder for the endpoint.
    /// </summary>
    /// <param name="app">Web application to which the endpoint will be mapped.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Thrown when the endpoint type is not supported.</exception>
    protected internal RouteHandlerBuilder GetRouteBuilder(WebApplication app)
    {
        return this.Type switch
        {
            EndpointTypes.Get => app.MapGet(this.Path, this.Handler),
            EndpointTypes.Post => app.MapPost(this.Path, this.Handler),
            EndpointTypes.Put => app.MapPut(this.Path, this.Handler),
            EndpointTypes.Delete => app.MapDelete(this.Path, this.Handler),
            _ => throw new ArgumentException("Invalid endpoint access type!"),
        };
    }
}
