namespace RpgBooks.Libraries.Templates.Abstractions;

/// <summary>
/// Interface for template renderer.
/// </summary>
public interface ITemplateRenderer
{
    /// <summary>
    /// Default layout model that will be used if no layout model is provided.
    /// </summary>
    ILayoutModel DefaultLayout { get; }

    /// <summary>
    /// Template renderer settings.
    /// </summary>
    TemplateSettings TemplateSettings { get; }

    /// <summary>
    /// Renders the template with the given model data with the default layout model data.
    /// </summary>
    /// <typeparam name="T">Type if the template model.</typeparam>
    /// <param name="model">Template model data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Rendered template</returns>
    Task<string> RenderAsync<T>(T model, CancellationToken cancellationToken = default!);

    /// <summary>
    /// Renders the template with the given model data and custom layout data.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="model"></param>
    /// <param name="layout"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string> RenderAsync<T>(T model, ILayoutModel layout, CancellationToken cancellationToken = default!);

    /// <summary>
    /// Sets the default layout model.
    /// </summary>
    /// <param name="layoutModel">Layout model data.</param>
    void SetDefaultLayoutModel(ILayoutModel layoutModel);

    /// <summary>
    /// Sets the template renderer settings.
    /// </summary>
    /// <param name="settings">Template renderer settings.</param>
    void SetSettings(TemplateSettings settings);
}
