namespace RpgBooks.Modules.Identity.Domain.Events;

using RpgBooks.Libraries.Module.Domain.Events;

public sealed record UserRegisteredEvent : BaseDomainEvent
{
    public UserRegisteredEvent(string email, string confirmationToken)
    {
        Email = email;
        ConfirmationToken = confirmationToken;
    }

    public string Email { get; init; } = default!;

    public string ConfirmationToken { get; init; } = default!;
}
