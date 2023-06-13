namespace RpgBooks.Libraries.Module.Presentation.Endpoints.Extensions;

using Cysharp.Text;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using RpgBooks.Libraries.Module.Presentation.Endpoints.Abstractions;
using RpgBooks.Libraries.Module.Presentation.Endpoints.Models;

/// <summary>
/// Web app extension methods.
/// </summary>
public static class WebAppExtensions
{
    /// <summary>
    /// Maps the given endpoint instance to the web application.
    /// </summary>
    /// <param name="app">Web app instance.</param>
    /// <param name="endpoint">Endpoint instance.</param>
    /// <param name="apiVersion">API Version</param>
    /// <returns></returns>
    public static RouteHandlerBuilder MapEndpoint(this WebApplication app, IApiEndpoint endpoint, int apiVersion = 1)
    {
        RouteHandlerBuilder builder = app.CreateRouteBuilder(endpoint, apiVersion);

        if (endpoint.Type == EndpointTypes.Get)
        {
            builder
                .Produces(StatusCodes.Status200OK, endpoint.ResponseType)
                .Produces(StatusCodes.Status404NotFound, typeof(ErrorResultModel));
        }
        else if (endpoint.Type == EndpointTypes.Post)
        {
            builder
                .Produces(StatusCodes.Status200OK, endpoint.ResponseType)
                .Produces(StatusCodes.Status400BadRequest, typeof(ErrorResultModel));
        }
        else if (endpoint.Type == EndpointTypes.Put)
        {
            builder
                .Produces(StatusCodes.Status200OK, endpoint.ResponseType)
                .Produces(StatusCodes.Status404NotFound, typeof(ErrorResultModel))
                .Produces(StatusCodes.Status400BadRequest, typeof(ErrorResultModel));
        }
        else if (endpoint.Type == EndpointTypes.Delete)
        {
            builder
                .Produces(StatusCodes.Status200OK, endpoint.ResponseType)
                .Produces(StatusCodes.Status404NotFound, typeof(ErrorResultModel))
                .Produces(StatusCodes.Status400BadRequest, typeof(ErrorResultModel));
        }

        return builder;
    }

    private static RouteHandlerBuilder CreateRouteBuilder(this WebApplication app, IApiEndpoint endpoint, int version)
    {
        var endpointType = endpoint.GetType();

        RouteHandlerBuilder builder = app.CreateInitialRouteBuilder(endpoint, version)
            .ApplyCaching(endpointType)
            .ApplyAuthorization(endpointType)
            .ApplyCacheRevoking(app, endpointType)
            .WithName(endpoint.Name)
            .WithTags(endpoint.Tag);
        return builder;
    }

    /// <summary>
    /// Gets the route builder for the endpoint.
    /// </summary>
    /// <param name="app">Web application to which the endpoint will be mapped.</param>
    /// <param name="apiEndpoint">API endpoint instance.</param>
    /// <param name="version">API version.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Thrown when the endpoint type is not supported.</exception>
    private static RouteHandlerBuilder CreateInitialRouteBuilder(this WebApplication app, IApiEndpoint apiEndpoint, int version)
    {
        string versionPrefix = ZString.Format("/api/v{0}", version);

        return apiEndpoint.Type switch
        {
            EndpointTypes.Get => app.MapGet(apiEndpoint.GetApiPath(versionPrefix), apiEndpoint.Handler),
            EndpointTypes.Post => app.MapPost(apiEndpoint.GetApiPath(versionPrefix), apiEndpoint.Handler),
            EndpointTypes.Put => app.MapPut(apiEndpoint.GetApiPath(versionPrefix), apiEndpoint.Handler),
            EndpointTypes.Delete => app.MapDelete(apiEndpoint.GetApiPath(versionPrefix), apiEndpoint.Handler),
            _ => throw new ArgumentException("Invalid endpoint access type!"),
        };
    }
}
