namespace RpgBooks.Libraries.Module.Infrastructure.Services.Email;

using RpgBooks.Libraries.Module.Application.Services.Email;
using RpgBooks.Libraries.Templates;

/// <summary>
/// Email template renderer that uses the Fluid template engine
/// </summary>
public sealed class EmailFluidTemplateRenderer : BaseFluidTemplateRenderer, IEmailTemplateRenderer
{
}
