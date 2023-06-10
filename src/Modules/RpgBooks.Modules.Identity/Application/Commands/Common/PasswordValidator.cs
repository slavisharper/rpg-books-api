namespace RpgBooks.Modules.Identity.Application.Commands.Common;

using FluentValidation;

using RpgBooks.Modules.Identity.Domain.Settings;

/// <summary>
/// Password validator.
/// </summary>
/// <typeparam name="TCommand">Type of the password command.</typeparam>
public class PasswordValidator<TCommand> : AbstractValidator<TCommand>
    where TCommand : IPasswordCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PasswordValidator{TCommand}"/> class.
    /// </summary>
    /// <param name="passwordStrengthSettings">Password strength settings.</param>
    public PasswordValidator(PasswordStrengthSettings passwordStrengthSettings)
    {
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(passwordStrengthSettings.MinPasswordLength)
            .MaximumLength(passwordStrengthSettings.MaxPasswordLength);

        if (passwordStrengthSettings.RequireDigit)
        {
            RuleFor(x => x.Password)
                .Matches(@"\d")
                .WithMessage("Password must contain at least one digit.");
        }

        if (passwordStrengthSettings.RequireLowercase)
        {
            RuleFor(x => x.Password)
                .Matches(@"[a-z]")
                .WithMessage("Password must contain at least one lowercase character.");
        }

        if (passwordStrengthSettings.RequireNonAlphanumeric)
        {
            RuleFor(x => x.Password)
                .Matches(@"\W")
                .WithMessage("Password must contain at least one non alphanumeric character.");
        }

        if (passwordStrengthSettings.RequireUppercase)
        {
            RuleFor(x => x.Password)
                .Matches(@"[A-Z]")
                .WithMessage("Password must contain at least one uppercase character.");
        }

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .Equal(x => x.Password);
    }
}
