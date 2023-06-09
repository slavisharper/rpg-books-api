namespace RpgBooks.Modules.Identity.Application.Commands.Register;

using RpgBooks.Libraries.Module.Application.Commands.Contracts;
using RpgBooks.Modules.Identity.Application.Commands.Common;

/// <summary>
/// Command request for registering a new user.
/// </summary>
public sealed record RegisterCommand : ICommand, IPasswordCommand
{
    /// <summary>
    /// Gets user email.
    /// </summary>
    public string Email { get; init; } = default!;

    /// <summary>
    /// Gets user password.
    /// </summary>
    public string Password { get; init; } = default!;

    /// <summary>
    /// Gets confirmed user password.
    /// </summary>
    public string ConfirmPassword { get; init; } = default!;
}
