namespace RpgBooks.Libraries.Templates;

using Cysharp.Text;

using Fluid;

using RpgBooks.Libraries.Templates.Abstractions;
using RpgBooks.Libraries.Templates.Exceptions;
using RpgBooks.Libraries.Templates.Models;

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Base fluid template renderer.
/// <para>This renderer should be inherited and registered with <see cref="RpgBooks.Libraries.Templates.Configuration.RegisterRenderer{TInterface, TRenderer}(Microsoft.Extensions.DependencyInjection.IServiceCollection, Action{TemplateSettings}?)"/></para>
/// </summary>
public abstract class BaseFluidTemplateRenderer : ITemplateRenderer
{
    private readonly HashSet<string> layouts = new();
    private readonly ConcurrentDictionary<string, ITemplateInfo> templates = new();
    private readonly ConcurrentDictionary<string, IFluidTemplate> parsedTemplates = new();

    private readonly FluidParser parser;
    private readonly TemplateOptions templateOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseFluidTemplateRenderer"/> class.
    /// </summary>
    public BaseFluidTemplateRenderer()
    {
        this.parser = new FluidParser();
        this.templateOptions = new TemplateOptions
        {
            MemberAccessStrategy = new UnsafeMemberAccessStrategy(),
        };

        this.DefaultLayout = default!;
        this.TemplateSettings = default!;

        this.SetSettings(new TemplateSettings());
    }

    /// <inheritdoc/>
    public ILayoutModel DefaultLayout { get; private set; }

    /// <inheritdoc/>
    public TemplateSettings TemplateSettings { get; private set; }

    /// <inheritdoc/>
    public virtual ValueTask<string> RenderAsync<T>(T model, CancellationToken cancellationToken)
        => RenderAsync(model, this.DefaultLayout, cancellationToken);

    /// <inheritdoc/>
    public async ValueTask<string> RenderAsync<T>(T model, ILayoutModel layoutModel, CancellationToken cancellationToken = default)
    {
        string modelName = typeof(T).FullName!;
        string templateName = this.GetTemplateName(modelName);
        string bodyContent = await this.RenderTemplate(templateName, model);

        layoutModel.Content = bodyContent;
        string layoutTemplateName = this.GetLayoutTemplateName(templateName);
        return await this.RenderTemplate(layoutTemplateName, layoutModel);
    }

    /// <inheritdoc/>
    public void SetDefaultLayoutModel(ILayoutModel layoutModel)
    {
        this.DefaultLayout = layoutModel;
    }

    /// <inheritdoc/>
    public void SetSettings(TemplateSettings settings)
    {
        this.TemplateSettings = settings;
        this.SetDefaultLayoutModel(settings.DefaultLayoutModel);
        this.ScanForTemplates(settings.TemplateAssemblyLocations);
    }

    private void ScanForTemplates(IEnumerable<Assembly> templateAssemblyLocations)
    {
        foreach (var assembly in templateAssemblyLocations)
        {
            var resources = assembly.GetEmbededResourceNames();
            foreach (var resource in resources)
            {
                if (resource.EndsWith(this.TemplateSettings.ViewNameWithExtensionSuffix))
                {
                    this.templates.TryAdd(resource, new EmbeddedTemplateInfo(resource, assembly));
                }

                if (resource.EndsWith(this.TemplateSettings.LayoutViewNameWithExtension))
                {
                    this.templates.TryAdd(resource, new EmbeddedTemplateInfo(resource, assembly));
                    this.layouts.Add(resource);
                }
            }
        }
    }

    private IFluidTemplate GetParsedTemplate(string resourceName)
    {
        if (parsedTemplates.ContainsKey(resourceName))
        {
            return parsedTemplates[resourceName];
        }

        var template = this.templates[resourceName];
        if (template is null)
        {
            throw new TemplateNotFoundException();
        }

        if (!this.parser.TryParse(template.Content, out IFluidTemplate parsedTemplate, out string? error))
        {
            throw new TemplateParseException(error);
        }

        parsedTemplates.TryAdd(resourceName, parsedTemplate);
        return parsedTemplate;
    }

    private string GetTemplateName(string modelName)
    {
        var templateName = modelName.Replace(
            this.TemplateSettings.DataModelNameSuffix, this.TemplateSettings.ViewNameSuffix);

        var resourceName = ZString.Format("{0}.{1}", templateName, TemplateSettings.TemplateExtension);
        return resourceName;
    }

    private string GetLayoutTemplateName(string resourceName)
    {
        int maxCount = 0;

        ReadOnlySpan<char> layoutResourceName = null;
        ReadOnlySpan<char> templateName = resourceName;
        foreach (ReadOnlySpan<char> layoutName in this.layouts)
        {
            int count = 0;
            for (int t = 0; t < templateName.Length && t < layoutName.Length; t++)
            {
                if (layoutName[t] == templateName[t])
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            if (count > maxCount || (count == maxCount && layoutName.Length < layoutResourceName.Length) || maxCount == 0)
            {
                maxCount = count;
                layoutResourceName = layoutName;
            }
        }

        return layoutResourceName.ToString();
    }

    private ValueTask<string> RenderTemplate<T>(string templateName, T data)
    {
        IFluidTemplate fluidTemplate = this.GetParsedTemplate(templateName);
        TemplateContext templateContext = new(data, this.templateOptions);
        return fluidTemplate.RenderAsync(templateContext);
    }
}
