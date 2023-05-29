namespace RpgBooks.Libraries.Module.Application.Results;

using RpgBooks.Libraries.Module.Application.Results.Contracts;

/// <summary>
/// Specifies the failure reason for <see cref="IAppResult"/>.
/// </summary>
public enum FailureReason
{
    /// <summary>
    /// Represent no failure.
    /// </summary>
    None = 0,

    /// <summary>
    /// Used for uncategorised failures.
    /// </summary>
    General = 1,

    /// <summary>
    /// Used for input validation failure.
    /// </summary>
    ValidationFailed = 2,

    /// <summary>
    /// Used for unauthorized access.
    /// </summary>
    Unauthorized = 3,

    /// <summary>
    /// Used for not found entity.
    /// </summary>
    NotFound = 4,
}
