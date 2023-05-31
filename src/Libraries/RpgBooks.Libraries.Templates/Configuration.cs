namespace RpgBooks.Libraries.Templates;

using Microsoft.Extensions.DependencyInjection;

using RpgBooks.Libraries.Templates.Abstractions;

/// <summary>
/// Template renderer configurations.
/// </summary>
public static class Configuration
{
    /// <summary>
    /// Registers a template renderer.
    /// </summary>
    /// <typeparam name="TInterface">Type of the interface of the renderer.</typeparam>
    /// <typeparam name="TRenderer">Implementation type of the renderer.</typeparam>
    /// <param name="services">Application services collection.</param>
    /// <param name="settingsFunc">Template renderer settings configuration.</param>
    /// <returns>Configured services collection.</returns>
    public static IServiceCollection RegisterRenderer<TInterface, TRenderer>(
        this IServiceCollection services,
        Action<TemplateSettings>? settingsFunc = null)
        where TInterface : ITemplateRenderer
        where TRenderer : class, TInterface, new()
    {
        var settings = new TemplateSettings();
        if (settingsFunc != null)
        {
            settingsFunc(settings);
        }

        var renderer = new TRenderer();
        renderer.SetSettings(settings);

        services.AddSingleton(typeof(TInterface), renderer);
        return services;
    }
}
