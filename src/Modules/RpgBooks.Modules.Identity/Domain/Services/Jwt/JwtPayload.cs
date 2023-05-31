namespace RpgBooks.Modules.Identity.Domain.Services.Jwt;

using RpgBooks.Libraries.Module.Infrastructure.Services.CurrentUser;

using System.Text.Json.Serialization;

/// <summary>
/// Represents the payload of a JWT.
/// </summary>
public sealed record JwtPayload
{
    /// <summary>
    /// Gets the JWT id.
    /// </summary>
    [JsonPropertyName(UserClaimTypes.JwtId)]
    public string JwtId { get; init; } = default!;

    /// <summary>
    /// Gets the valid issuer.
    /// </summary>
    [JsonPropertyName(UserClaimTypes.Issuer)]
    public string Issuer { get; init; } = default!;

    /// <summary>
    /// Gets the valid audience.
    /// </summary>
    [JsonPropertyName(UserClaimTypes.Audience)]
    public string Audience { get; init; } = default!;

    /// <summary>
    /// Gets the issued at time.
    /// </summary>
    [JsonPropertyName(UserClaimTypes.IssuedAt)]
    public long IssuedAt { get; init; } = default!;

    /// <summary>
    /// Gets the expiration time.
    /// </summary>
    [JsonPropertyName(UserClaimTypes.Expiration)]
    public long ExpiprationTime { get; init; }

    /// <summary>
    /// Gets the not before time.
    /// </summary>
    [JsonPropertyName(UserClaimTypes.NotBefore)]
    public long NotBefore { get; init; } = default!;

    /// <summary>
    /// Gets the user security stamp.
    /// </summary>
    [JsonPropertyName(UserClaimTypes.SecurityStamp)]
    public string? SecurityStamp { get; init; } = default!;

    /// <summary>
    /// Gets the session id.
    /// </summary>
    [JsonPropertyName(UserClaimTypes.SessionId)]
    public string? SessionId { get; init; } = default!;

    /// <summary>
    /// Gets the user id.
    /// </summary>
    [JsonPropertyName(UserClaimTypes.UId)]
    public string Uid { get; init; } = default!;

    /// <summary>
    /// Gets the user title.
    /// </summary>
    [JsonPropertyName(UserClaimTypes.Title)]
    public string? Title { get; init; }

    /// <summary>
    /// Gets the user first name.
    /// </summary>
    [JsonPropertyName(UserClaimTypes.FirstName)]
    public string? FirstName { get; init; }

    /// <summary>
    /// Gets the user family name.
    /// </summary>
    [JsonPropertyName(UserClaimTypes.LastName)]
    public string? LastName { get; init; }

    /// <summary>
    /// Gets the user middle name.
    /// </summary>
    [JsonPropertyName(UserClaimTypes.MiddleName)]
    public string? MiddleName { get; init; }

    /// <summary>
    /// Gets the user email.
    /// </summary>
    [JsonPropertyName(UserClaimTypes.Email)]
    public string Email { get; init; } = default!;

    /// <summary>
    /// Gets the user email verified status.
    /// </summary>
    [JsonPropertyName(UserClaimTypes.EmailVerified)]
    public bool EmailVerified { get; init; } = default!;

    /// <summary>
    /// Gets the user phone number verified status.
    /// </summary>
    [JsonPropertyName(UserClaimTypes.PhoneNumberVerified)]
    public bool PhoneNumberVerified { get; init; } = default!;

    /// <summary>
    /// Gets the user roles.
    /// </summary>
    [JsonPropertyName(UserClaimTypes.Roles)]
    public string[] Roles { get; init; } = default!;

    /// <summary>
    /// Gets the custom claims.
    /// </summary>
    [JsonPropertyName(UserClaimTypes.CustomClaims)]
    public Dictionary<string, string?> CustomClaims { get; init; } = default!;
}
