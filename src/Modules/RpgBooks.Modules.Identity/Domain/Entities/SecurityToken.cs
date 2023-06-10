namespace RpgBooks.Modules.Identity.Domain.Entities;

using RpgBooks.Modules.Identity.Domain.Validation;
using RpgBooks.Libraries.Module.Domain.Entities;

/// <summary>
/// User security token.
/// </summary>
public sealed class SecurityToken : BaseEntity<int>
{
    internal SecurityToken(string value, SecurityTokenType tokenType, string? sessionId = null)
    {
        this.Value = value;
        this.TokenType = tokenType;
        this.Created = DateTimeOffset.UtcNow;
        this.SessionId = sessionId;

        this.Validate();
    }

    internal SecurityToken(string value, SecurityTokenType tokenType, TimeSpan validity, string? sessionId = null)
        : this(value, tokenType, sessionId)
    {
        this.ExpirationTime = DateTimeOffset.UtcNow.Add(validity!);
    }

    private SecurityToken(string value, DateTimeOffset created, DateTimeOffset? expirationTime, string? sessionId)
    {
        this.Value= value;
        this.Created= created;
        this.ExpirationTime= expirationTime;
        this.SessionId = sessionId;

        this.TokenType= default!;
    }

    /// <summary>
    /// Gets the encrypted token value.
    /// </summary>
    public string Value { get; private set; } = default!;

    /// <summary>
    /// Gets the date of creation of the token.
    /// </summary>
    public DateTimeOffset Created { get; private set; }

    /// <summary>
    /// Gets token expiration time.
    /// </summary>
    public DateTimeOffset? ExpirationTime { get; private set; }
    
    /// <summary>
    /// Gets the token type. This specifies the token responsibility and purpose.
    /// </summary>
    public SecurityTokenType TokenType { get; private set; }

    /// <summary>
    /// Gets the token session Id.
    /// <para>If this field is populated the token will be responsible only for the given session.</para>
    /// <para>Otherwise the token will be responsible for all of the user sessions.</para>
    /// <para>This will be helpful to maintain unique tokens when user uses different browsers or devices.</para>
    /// </summary>
    public string? SessionId { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the token is expired.
    /// </summary>
    public bool IsExpired => this.ExpirationTime is not null && this.ExpirationTime < DateTimeOffset.UtcNow;

    private void Validate()
    {
        SecurityTokenValidation.EnsureThat.HasValidToken(this.Value);
    }   
}
