namespace RpgBooks.Libraries.Templates;

using Microsoft.Extensions.DependencyInjection;

using RpgBooks.Libraries.Templates.Abstractions;

public static class Configuration
{
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
