namespace RpgBooks.Libraries.Module.Presentation.Endpoints.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.DependencyInjection;

using RpgBooks.Libraries.Module.Presentation.Endpoints.Attributes;
using RpgBooks.Libraries.Module.Presentation.Endpoints.Filters;

using System;
using System.Reflection;

/// <summary>
/// Route builder extensions.
/// </summary>
internal static class RouteHandlerBuilderExtensions
{
    /// <summary>
    /// Apply authorization over given endpoint builder.
    /// </summary>
    /// <param name="builder">Route builder.</param>
    /// <param name="endpointType">Endpoint type.</param>
    /// <returns>Configured route builder.</returns>
    internal static RouteHandlerBuilder ApplyAuthorization(this RouteHandlerBuilder builder, Type endpointType)
    {
        var authorizeAttribute = endpointType.GetCustomAttribute<AuthorizeEndpointAttribute>();
        if (authorizeAttribute is null)
        {
            return builder;
        }

        var policies = authorizeAttribute.PolicyNames;
        if (policies is not null && policies.Any())
        {
            builder.RequireAuthorization(policies);
        }
        else
        {
            builder.RequireAuthorization();
        }

        return builder.Produces(StatusCodes.Status401Unauthorized);
    }

    /// <summary>
    /// Apply caching over given endpoint builder.
    /// </summary>
    /// <param name="builder">Route builder.</param>
    /// <param name="endpointType">Endpoint type.</param>
    /// <returns>Configured route builder.</returns>
    internal static RouteHandlerBuilder ApplyCaching(this RouteHandlerBuilder builder, Type endpointType)
    {
        var cacheAttribute = endpointType.GetCustomAttribute<CacheEndpointAttribute>();
        if (cacheAttribute is null)
        {
            return builder;
        }

        builder.CacheOutput(c => c.Tag(endpointType.FullName!));
        return builder;
    }

    /// <summary>
    /// Apply cache revoking logic over given endpoint builder.
    /// </summary>
    /// <param name="builder">Route builder.</param>
    /// <param name="app">Web application.</param>
    /// <param name="endpointType">Endpoint type.</param>
    /// <returns>Configured route builder.</returns>
    internal static RouteHandlerBuilder ApplyCacheRevoking(this RouteHandlerBuilder builder, WebApplication app, Type endpointType)
    {
        var cacheAttribute = endpointType.GetCustomAttribute<RevokeCachedEndpointAttribute>();
        if (cacheAttribute is null)
        {
            return builder;
        }

        var cacheStore = app.Services.GetRequiredService<IOutputCacheStore>();
        builder.AddEndpointFilter(new RevokeCacheFilter(cacheStore, cacheAttribute.RevokeKey));
        return builder;
    }
}
