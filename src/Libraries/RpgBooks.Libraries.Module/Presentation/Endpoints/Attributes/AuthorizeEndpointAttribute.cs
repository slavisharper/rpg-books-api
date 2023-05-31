namespace RpgBooks.Libraries.Module.Presentation.Endpoints.Attributes;
/// <summary>
/// Authorize endpoint attribute.
/// </summary>
[AttributeUsage(AttributeTargets.All)]
public sealed class AuthorizeEndpointAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorizeEndpointAttribute"/> class.
    /// </summary>
    public AuthorizeEndpointAttribute()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorizeEndpointAttribute"/> class.
    /// </summary>
    /// <param name="policies">Authorization policies.</param>
    public AuthorizeEndpointAttribute(params string[] policies)
    {
        this.PolicyNames = policies;
    }

    /// <summary>
    /// Gets authorization policies.
    /// </summary>
    public string[]? PolicyNames { get; init; }
}
