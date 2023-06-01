namespace RpgBooks.Modules.Identity.Application;

using Microsoft.Extensions.DependencyInjection;

using RpgBooks.Libraries.Module.Application;
using RpgBooks.Libraries.Module.Presentation;

/// <summary>
/// Identity application layer configuration.
/// </summary>
internal static class IdentityApplicationConfiguration
{
    /// <summary>
    /// Adds the identity application layer.
    /// </summary>
    /// <param name="services">Application services.</param>
    /// <returns>Configured application services.</returns>
    public static IServiceCollection AddIdentityApplicationLayer(this IServiceCollection services)
        => services
            .AddCQRS(typeof(IdentityApplicationConfiguration).Assembly)
            .AddApiEndpoints(typeof(IdentityApplicationConfiguration).Assembly);
}
