namespace RpgBooks.Modules.Identity.Application.Commands.Common;

/// <summary>
/// Command request containing password and confirmed password.
/// </summary>
public interface IPasswordCommand
{
    /// <summary>
    /// Gets the password.
    /// </summary>
    string Password { get; }

    /// <summary>
    /// Gets the confirmed password.
    /// </summary>
    string ConfirmPassword { get; }
}
