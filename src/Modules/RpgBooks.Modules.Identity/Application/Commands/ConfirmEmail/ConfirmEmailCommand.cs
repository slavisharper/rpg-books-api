namespace RpgBooks.Modules.Identity.Application.Commands.ConfirmEmail;

using RpgBooks.Libraries.Module.Application.Commands.Contracts;

/// <summary>
/// Command to confirm a user's email address.
/// </summary>
/// <param name="UserId">User identifier.</param>
/// <param name="Token">Email confirmation token.</param>
public sealed record ConfirmEmailCommand(int UserId, string Token) : ICommand;