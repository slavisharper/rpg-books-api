namespace RpgBooks.Libraries.Module.Presentation.Endpoints.Attributes;
/// <summary>
/// Revokes cached response for given endpoint after the successful execution of the target endpoint.
/// </summary>
[AttributeUsage(AttributeTargets.All)]
public sealed class RevokeCachedEndpointAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RevokeCachedEndpointAttribute"/> class.
    /// </summary>
    /// <param name="endpointType">Type of the revoked endpoint.</param>
    public RevokeCachedEndpointAttribute(Type endpointType)
    {
        this.RevokeKey = endpointType.FullName!;
    }

    /// <summary>
    /// Gets the key of an endpoint that has cache and will be revoked.
    /// </summary>
    public string RevokeKey { get; }
}
