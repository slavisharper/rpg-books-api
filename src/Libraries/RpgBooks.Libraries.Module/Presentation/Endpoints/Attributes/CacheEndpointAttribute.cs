namespace RpgBooks.Libraries.Module.Presentation.Endpoints.Attributes;

/// <summary>
/// Cache endpoint output attribute.
/// Authorized endpoints are not cached. Also requests with cookies or bearer token are also not cached.
/// </summary>
[AttributeUsage(AttributeTargets.All)]
public sealed class CacheEndpointAttribute : Attribute
{
}
