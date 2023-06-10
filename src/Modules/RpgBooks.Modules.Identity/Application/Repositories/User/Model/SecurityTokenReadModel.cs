namespace RpgBooks.Modules.Identity.Application.Repositories.User.Model;

using RpgBooks.Modules.Identity.Domain.Entities;

/// <summary>
/// Security token read-only model.
/// </summary>
public sealed record SecurityTokenReadModel
{
    /// <summary>
    /// Gets the encrypted token value.
    /// </summary>
    public string Value { get; init; } = default!;

    /// <summary>
    /// Gets the date of creation of the token.
    /// </summary>
    public DateTimeOffset Created { get; init; }

    /// <summary>
    /// Gets token expiration time.
    /// </summary>
    public DateTimeOffset? ExpirationTime { get; init; }

    /// <summary>
    /// Gets the token type.
    /// </summary>
    public int TokenTypeValue { get; init; }

    /// <summary>
    /// Gets the token session Id.
    /// </summary>
    public string? SessionId { get; init; }

    /// <summary>
    /// Gets a value indicating whether the token is expired.
    /// </summary>
    public bool IsExpired => this.ExpirationTime is not null && this.ExpirationTime < DateTimeOffset.UtcNow;
}
