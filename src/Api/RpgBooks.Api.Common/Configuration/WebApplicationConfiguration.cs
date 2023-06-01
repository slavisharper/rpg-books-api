namespace RpgBooks.Api.Common.Configuration;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;

using RpgBooks.Libraries.Module.Application.Services;
using RpgBooks.Libraries.Module.Infrastructure.Persistence.Abstractions;
using RpgBooks.Libraries.Module.Presentation.Endpoints.Abstractions;

/// <summary>
/// Web Application configuration.
/// </summary>
public static class WebApplicationConfiguration
{
    /// <summary>
    /// Configures the web application.
    /// </summary>
    /// <param name="app">Web application instance.</param>
    /// <returns>Configured <see cref="WebApplication"/>.</returns>
    public static WebApplication UseWebAppConfiguration(this WebApplication app)
    {
        app.UseHttpsRedirection()
           .UseCors(options => options
               .AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod()
               .WithExposedHeaders(HeaderNames.ContentDisposition))
           .UseOutputCache()
           .UseAuthentication()
           .UseAuthorization()
           .UseSwagger()
           .UseSwaggerUI();

        using var serviceScope = app.Services.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;

        app.UseApiEndpoints(serviceProvider)
           .UseDbInitializers(serviceProvider);

        return app;
    }

    private static WebApplication UseApiEndpoints(
        this WebApplication app,
        IServiceProvider serviceProvider)
    {
        var endpoints = serviceProvider.GetServices<IApiEndpoint>();
        var urlProvider = serviceProvider.GetRequiredService<IUrlProvider>();

        foreach (var endpoint in endpoints)
        {
            var type = endpoint.GetType();
            var requestArgumentType = type?.BaseType?.GetGenericArguments().FirstOrDefault();
            if (requestArgumentType is not null)
            {
                urlProvider.AddEndpointRequestPair(endpoint.Name, requestArgumentType);
            }

            endpoint.Register(app);
        }

        return app;
    }

    private static IApplicationBuilder UseDbInitializers(
        this IApplicationBuilder app,
        IServiceProvider serviceProvider)
    {
        var dbInitializers = serviceProvider.GetServices<IDbInitializer>();

        foreach (var initializer in dbInitializers)
        {
            initializer.Initialize();
        }

        return app;
    }
}
