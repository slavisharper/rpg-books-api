namespace RpgBooks.Modules.Identity.Application.Commands.ChangePassword;

using RpgBooks.Libraries.Module.Application.Commands.Contracts;
using RpgBooks.Modules.Identity.Application.Commands.Common;

/// <summary>
/// Change password command request.
/// </summary>
/// <param name="OldPassword">Old password value.</param>
/// <param name="Password">New password value.</param>
/// <param name="ConfirmPassword">Confirm new password value.</param>
public sealed record ChangePasswordCommand(
    string OldPassword,
    string Password,
    string ConfirmPassword) : ICommand, IPasswordCommand;