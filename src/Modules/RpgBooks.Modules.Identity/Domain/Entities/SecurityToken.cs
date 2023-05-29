namespace RpgBooks.Modules.Identity.Domain.Entities;

using RpgBooks.Modules.Identity.Domain.Validation;
using RpgBooks.Libraries.Module.Domain.Entities;

public sealed class SecurityToken : BaseEntity<int>
{
    internal SecurityToken(string value, SecurityTokenType tokenType, User user)
    {
        this.Value = value;
        this.User = user;
        this.TokenType = tokenType;
        this.Created = DateTimeOffset.UtcNow;

        this.Validate();
    }

    internal SecurityToken(string value, SecurityTokenType tokenType, User user, TimeSpan validity)
        : this(value, tokenType, user)
    {
        this.ExpirationTime = DateTimeOffset.UtcNow.Add(validity!);
    }

    private SecurityToken(string value, DateTimeOffset created, DateTimeOffset? expirationTime)
    {
        this.Value= value;
        this.Created= created;
        this.ExpirationTime= expirationTime;

        this.User = default!;
        this.TokenType= default!;
    }

    /// <summary>
    /// Gets the encrypted token value.
    /// </summary>
    public string Value { get; private set; } = default!;

    /// <summary>
    /// Gets the an user security token.
    /// </summary>
    public User User { get; private set; }

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

    private void Validate()
    {
        SecurityTokenValidation.EnsureThat.HasValidToken(this.Value);
    }   
}
