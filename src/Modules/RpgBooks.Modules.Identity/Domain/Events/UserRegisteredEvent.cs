namespace RpgBooks.Modules.Identity.Domain.Events;

using RpgBooks.Libraries.Module.Domain.Events;

/// <summary>
/// Event raised when a user is registered.
/// </summary>
public sealed record UserRegisteredEvent : BaseDomainEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserRegisteredEvent"/> class.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="emailConfirmationToken">The email confirmation token</param>
    public UserRegisteredEvent(string email, string emailConfirmationToken)
    {
        Email = email;
        EmailConfirmationToken = emailConfirmationToken;
    }

    /// <summary>
    /// Gets the user email.
    /// </summary>
    public string Email { get; init; } = default!;

    /// <summary>
    /// Gets the email confirmation token.
    /// </summary>
    public string EmailConfirmationToken { get; init; } = default!;
}
