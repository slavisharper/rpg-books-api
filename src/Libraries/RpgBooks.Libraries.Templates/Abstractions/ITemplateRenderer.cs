namespace RpgBooks.Libraries.Templates.Abstractions;

public interface ITemplateRenderer
{
    ILayoutModel DefaultLayout { get; }

    TemplateSettings TemplateSettings { get; }

    Task<string> RenderAsync<T>(T model, CancellationToken cancellationToken = default!);

    Task<string> RenderAsync<T>(T model, ILayoutModel layout, CancellationToken cancellationToken = default!);

    void SetDefaultLayoutModel(ILayoutModel layoutModel);

    void SetSettings(TemplateSettings settings);
}
