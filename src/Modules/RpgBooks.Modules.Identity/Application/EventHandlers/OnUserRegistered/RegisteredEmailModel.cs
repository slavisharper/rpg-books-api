namespace RpgBooks.Modules.Identity.Application.EventHandlers.OnUserRegistered;

/// <summary>
/// Registered email model used for generating the email body.
/// </summary>
/// <param name="Email">User email.</param>
/// <param name="ConfirmationToken">Email confirmation token.</param>
internal sealed record RegisteredEmailModel(string Email, string ConfirmationToken);