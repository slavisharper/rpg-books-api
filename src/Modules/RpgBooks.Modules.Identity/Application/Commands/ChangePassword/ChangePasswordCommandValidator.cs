namespace RpgBooks.Modules.Identity.Application.Commands.ChangePassword;

using FluentValidation;

using RpgBooks.Modules.Identity.Application.Commands.Common;
using RpgBooks.Modules.Identity.Domain.Settings;

/// <summary>
/// Change password command request validator.
/// </summary>
public sealed class ChangePasswordCommandValidator : PasswordValidator<ChangePasswordCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ChangePasswordCommandValidator"/> class.
    /// </summary>
    /// <param name="settingsOptions">Password strength settings.</param>
    public ChangePasswordCommandValidator(IOptions<PasswordStrengthSettings> settingsOptions)
        : base(settingsOptions.Value)
    {
        this.RuleFor(m => m.Password)
            .NotEqual(c => c.OldPassword)
            .NotEmpty();
    }
}
