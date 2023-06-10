namespace RpgBooks.Modules.Identity.Application.Commands.Login;

/// <summary>
/// Command request fro login with a user email and a password.
/// </summary>
public sealed record LoginCommand : ICommand
{
    /// <summary>
    /// Gets user email.
    /// </summary>
    public string Email { get; init; } = default!;

    /// <summary>
    /// Gets user account password.
    /// </summary>
    public string Password { get; init; } = default!;
}
