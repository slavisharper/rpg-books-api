namespace RpgBooks.Libraries.Templates;

using Cysharp.Text;

using RpgBooks.Libraries.Templates.Abstractions;
using RpgBooks.Libraries.Templates.Models;

using System.Reflection;

/// <summary>
/// Template settings used by the template renderer.
/// </summary>
public sealed class TemplateSettings
{
    /// <summary>
    /// Gets template files extension excluding the dot.
    /// </summary>
    public string TemplateExtension { get; private set; } = "html";

    /// <summary>
    /// Gets layout template name without the file extension.
    /// </summary>
    public string LayoutViewName { get; private set; } = "_Layout";

    /// <summary>
    /// Gets layout template name with the file extension.
    /// </summary>
    public string LayoutViewNameWithExtension => ZString.Format("{0}.{1}", this.LayoutViewName, this.TemplateExtension);

    /// <summary>
    /// Gets layout file name with the extension.
    /// </summary>
    public string LayoutViewFileName => ZString.Format("{0}.{1}", this.LayoutViewName, this.TemplateExtension);

    /// <summary>
    /// Gets the common template file name suffix.
    /// </summary>
    public string ViewNameSuffix { get; private set; } = "Template";

    /// <summary>
    /// Gets the common template file name ending with the extension.
    /// </summary>
    public string ViewNameWithExtensionSuffix => ZString.Format("{0}.{1}", this.ViewNameSuffix, this.TemplateExtension);

    /// <summary>
    /// Gets the common data class file name suffix.
    /// </summary>
    public string DataModelNameSuffix { get; private set; } = "Model";

    /// <summary>
    /// Gets the default layout model.
    /// </summary>
    public ILayoutModel DefaultLayoutModel { get; private set; } = new BaseLayoutModel();

    /// <summary>
    /// Gets the assemblies where the templates are located.
    /// </summary>
    public IEnumerable<Assembly> TemplateAssemblyLocations { get; private set; } = new Assembly[] { Assembly.GetEntryAssembly()! };

    /// <summary>
    /// Sets the default layout model.
    /// </summary>
    /// <param name="layoutModel">Layout model data.</param>
    public void SetDefaultLayoutModel(ILayoutModel layoutModel)
    {
        this.DefaultLayoutModel = layoutModel;
    }

    /// <summary>
    /// Sets assemblies containing the templates.
    /// </summary>
    /// <param name="assemblies"></param>
    public void SetTemplateAssemblies(params Assembly[] assemblies)
    {
        this.TemplateAssemblyLocations = assemblies;
    }
}
