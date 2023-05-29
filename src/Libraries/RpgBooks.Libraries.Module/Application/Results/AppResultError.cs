namespace RpgBooks.Libraries.Module.Application.Results;

using RpgBooks.Libraries.Module.Application.Results.Contracts;

/// <summary>
/// Represents error details in <see cref="AppResult"/>.
/// </summary>
public sealed class AppResultError : IError
{
    private readonly HashSet<string> errorMessages;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppResultError"/> class.
    /// </summary>
    /// <param name="key">Error key.</param>
    /// <param name="errorMessage">Error message.</param>
    public AppResultError(string key, string errorMessage)
    {
        this.Key = key;
        this.errorMessages = new HashSet<string> { errorMessage };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AppResultError"/> class.
    /// </summary>
    /// <param name="key">Error key.</param>
    /// <param name="errorMessages">Error messages.</param>
    public AppResultError(string key, params string[] errorMessages)
    {
        this.Key = key;
        this.errorMessages = new HashSet<string>(errorMessages);
    }

    /// <inheritdoc/>
    public string Key { get; init; }

    /// <inheritdoc/>
    public IReadOnlyCollection<string> ErrorMessages => this.errorMessages;

    /// <inheritdoc/>
    public void AddErrorMessage(string message)
    {
        this.errorMessages.Add(message);
    }

    /// <inheritdoc/>
    public void AddErrorMessages(IEnumerable<string> errorMessages)
    {
        foreach (var message in errorMessages)
        {
            this.errorMessages.Add(message);
        }
    }
}
