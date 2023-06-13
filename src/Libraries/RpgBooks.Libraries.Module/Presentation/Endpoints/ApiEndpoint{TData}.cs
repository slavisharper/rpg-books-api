namespace RpgBooks.Libraries.Module.Presentation.Endpoints;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using RpgBooks.Libraries.Module.Presentation.Endpoints.Extensions;
using RpgBooks.Libraries.Module.Presentation.Endpoints.Models;

using System.File;

/// <summary>
/// Base minimal API endpoint configuration class.
/// </summary>
/// <typeparam name="TRequest">Type of the request data.</typeparam>
/// <typeparam name="TResponseData">Type of the success response data.</typeparam>
public abstract class ApiEndpoint<TRequest, TResponseData> : ApiEndpoint<TRequest>
    where TRequest : notnull
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApiEndpoint{TRequest, TResponseData}"/> class.
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
        : base(path, name, tag, type, handler)
    {
    }

    /// <inheritdoc />
    public override Type ResponseType => typeof(SuccessResultModel<TResponseData>);
}
