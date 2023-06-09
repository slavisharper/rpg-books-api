namespace RpgBooks.Libraries.Module.Application.Services;

/// <summary>
/// HTTP utilities service.
/// </summary>
public interface IHttpUtilities
{
    /// <summary>
    /// Encode URL string.
    /// </summary>
    /// <param name="url">URL value.</param>
    /// <returns>Encoded URL.</returns>
    string? UrlEncode(string? url);

    /// <summary>
    /// Decode URL string.
    /// </summary>
    /// <param name="url">URL value.</param>
    /// <returns>Decoded URL.</returns>
    string? UrlDecode(string? url);
}
