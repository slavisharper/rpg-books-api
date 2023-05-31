namespace RpgBooks.Libraries.Module.Application.Services;

/// <summary>
/// URL provider for commands and queries.
/// </summary>
public interface IUrlProvider
{
    /// <summary>
    /// Get URL string for given command or query request.
    /// </summary>
    /// <typeparam name="TRequest">Type of the command or query request.</typeparam>
    /// <returns>The found URL string registered in endpoint. Returns null if not found.</returns>
    string? GetRequestUrl<TRequest>();

    /// <summary>
    /// Get URL string for given query request. This accepts object with query filter parameters.
    /// </summary>
    /// <typeparam name="TRequest">Type of the command or query request.</typeparam>
    /// <param name="args">Name of the registered endpoint.</param>
    /// <returns>The found URL string registered in endpoint. Returns null if not found.</returns>
    string? GetRequestUrl<TRequest>(TRequest args);

    /// <summary>
    /// Get URL string for given command or query request.
    /// </summary>
    /// <param name="requestType">Type of the request.</param>
    /// <returns>The found URL string registered in endpoint. Returns null if not found.</returns>
    string? GetRequestUrl(Type requestType);

    /// <summary>
    /// Get URL string for given command or query request.
    /// </summary>
    /// <param name="requestType">Type of the request.</param>
    /// <param name="args">Name of the registered endpoint.</param>
    /// <returns>The found URL string registered in endpoint. Returns null if not found.</returns>
    string? GetRequestUrl(Type requestType, object args);

    /// <summary>
    /// Adds pair of endpoint unique name and its corresponding input request model type.
    /// </summary>
    /// <param name="endpointName">Name of the registered endpoint.</param>
    /// <param name="requestType">Type of the mediator request.</param>
    void AddEndpointRequestPair(string endpointName, Type requestType);
}
