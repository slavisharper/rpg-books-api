namespace RpgBooks.Libraries.Module.Application.Results.Contracts;

/// <summary>
/// Represents an error details.
/// </summary>
public interface IError
{
    /// <summary>
    /// Gets the Error Key.
    /// </summary>
    string Key { get; }

    /// <summary>
    /// Gets the Error Messages.
    /// </summary>
    IReadOnlyCollection<string> ErrorMessages { get; }

    /// <summary>
    /// Add error message to given error key.
    /// </summary>
    /// <param name="message">Error message.</param>
    void AddErrorMessage(string message);

    /// <summary>
    /// Add error messages to given error key.
    /// </summary>
    /// <param name="errorMessages">Error messages.</param>
    void AddErrorMessages(IEnumerable<string> errorMessages);
}
