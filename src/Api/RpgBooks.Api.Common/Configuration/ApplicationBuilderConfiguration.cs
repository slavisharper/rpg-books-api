namespace RpgBooks.Api.Common.Configuration;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using RpgBooks.Libraries.Module.Application;
using RpgBooks.Libraries.Module.Infrastructure;
using RpgBooks.Modules.Identity;

using System.Reflection;

/// <summary>
/// Web Application builder configuration.
/// </summary>
public static class ApplicationBuilderConfiguration
{
    /// <summary>
    /// Add common web application configuration.
    /// <para>This registers the following services:</para>
    /// <para>- CORS</para>
    /// <para>- Application settings</para>
    /// <para>- Application authentication</para>
    /// <para>- Application authorization</para>
    /// <para>- Output cache</para>
    /// <para>- Endpoints API explorer</para>
    /// <para>- Swagger</para>
    /// <para>- Logging</para>
    /// <para>- Common required services</para>
    /// <para>- HTTP context accessor</para>
    /// </summary>
    /// <param name="builder">Web application builder.</param>
    /// <returns></returns>
    public static WebApplicationBuilder AddWebAppConfiguration(this WebApplicationBuilder builder)
    {
        var emailAssemblies = new Assembly[]
        {
            typeof(IdentityModuleConfiguration).Assembly,
            typeof(ApplicationConfiguration).Assembly,
        };

        builder.Services
            .AddCors()
            .AddApplicationSettings(builder.Configuration)
            .AddAppAuthentication(builder.Configuration)
            .AddAppAuthorization()
            .AddOutputCache()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddZLogging()
            .AddCommonRequiredServices(builder.Configuration, emailAssemblies)
            .TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        return builder;
    }
}
