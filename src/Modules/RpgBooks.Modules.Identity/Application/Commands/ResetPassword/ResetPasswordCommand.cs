namespace RpgBooks.Modules.Identity.Application.Commands.ResetPassword;

using RpgBooks.Modules.Identity.Application.Commands.Common;

/// <summary>
/// Reset password command request.
/// </summary>
public sealed record ResetPasswordCommand : ICommand, IPasswordCommand
{
    /// <summary>
    /// Gets the user email.
    /// </summary>
    public string Email { get; init; } = default!;

    /// <summary>
    /// Gets the provided reset password token value that is URL encoded.
    /// </summary>
    public string ResetToken { get; init; } = default!;

    /// <summary>
    /// Gets the new password.
    /// </summary>
    public string Password { get; init; } = default!;

    /// <summary>
    /// Gets the new password confirmation.
    /// </summary>
    public string ConfirmPassword { get; init; } = default!;
}
