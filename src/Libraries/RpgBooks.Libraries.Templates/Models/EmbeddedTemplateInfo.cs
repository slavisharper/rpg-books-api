namespace RpgBooks.Libraries.Templates.Models;

using RpgBooks.Libraries.Templates.Abstractions;

using System.Reflection;

/// <summary>
/// Template info for embedded file templates.
/// </summary>
public sealed class EmbeddedTemplateInfo : ITemplateInfo
{
    private readonly string resourceName;
    private readonly Assembly resourceAssembly;

    private string? content;
    private bool resourceRead;

    /// <summary>
    /// Initializes a new instance of the <see cref="EmbeddedTemplateInfo"/> class.
    /// </summary>
    /// <param name="resourceName">Manifest resource name.</param>
    /// <param name="resourceAssembly">Manifest resource assembly.</param>
    public EmbeddedTemplateInfo(string resourceName, Assembly resourceAssembly)
    {
        this.resourceName = resourceName;
        this.resourceAssembly = resourceAssembly;
    }

    /// <inheritdoc/>
    public string Name => this.resourceName;

    /// <inheritdoc/>
    public string? Content
    {
        get
        {
            if (this.resourceRead)
            {
                return this.content;
            }

            this.content = this.ReadEmbeddedResource();
            this.resourceRead = true;
            return this.content;
        }
    }

    private string? ReadEmbeddedResource()
    {
        if (this.resourceName is null ||
            this.resourceAssembly is null)
        {
            return null;
        }

        using var resourceStream = this.resourceAssembly.GetManifestResourceStream(this.resourceName);
        if (resourceStream is null)
        {
            return null;
        }

        using var streamReader = new StreamReader(resourceStream);
        return streamReader.ReadToEnd();
    }
}
