namespace RpgBooks.Modules.Identity.Application.Commands.ResetPassword;

using FluentValidation;

using RpgBooks.Modules.Identity.Application.Commands.Common;
using RpgBooks.Modules.Identity.Domain.Settings;

/// <summary>
/// Validator for <see cref="ResetPasswordCommand"/>.
/// </summary>
public sealed class ResetPasswordCommandValidator : PasswordValidator<ResetPasswordCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResetPasswordCommandValidator"/> class.
    /// </summary>
    /// <param name="passwordStrengthSettings"></param>
    public ResetPasswordCommandValidator(IOptions<PasswordStrengthSettings> passwordStrengthSettings)
        : base(passwordStrengthSettings.Value)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.ResetToken)
            .NotEmpty();
    }
}
