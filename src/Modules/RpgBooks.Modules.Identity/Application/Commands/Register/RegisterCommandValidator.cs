namespace RpgBooks.Modules.Identity.Application.Commands.Register;

using FluentValidation;

using RpgBooks.Modules.Identity.Application.Resources;
using RpgBooks.Modules.Identity.Domain.Repositories;
using RpgBooks.Modules.Identity.Domain.Settings;

using Microsoft.Extensions.Options;

/// <summary>
/// Register command request validator.
/// </summary>
public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterCommandValidator"/> class.
    /// </summary>
    /// <param name="userRepository">User domain repository.</param>
    /// <param name="identitySettings">Identity module settings.</param>
    public RegisterCommandValidator(
        IUserDomainRepository userRepository,
        IOptions<IdentitySettings> identitySettings)
    {
        var passwordOptions = identitySettings.Value.PasswordStrengthSettings;

        ValidateEmail(userRepository);

        ValidatePassword(passwordOptions);
    }

    private void ValidateEmail(IUserDomainRepository userRepository)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Email)
            .MustAsync(async (email, cancellation) => !(await userRepository.Exists(email, cancellation)))
            .WithMessage(Messages.EmailTakenError);
    }

    private void ValidatePassword(PasswordStrengthSettings passwordOptions)
    {
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(passwordOptions.MinPasswordLength)
            .MaximumLength(passwordOptions.MaxPasswordLength);

        if (passwordOptions.RequireDigit)
        {
            RuleFor(x => x.Password)
                .Matches(@"\d")
                .WithMessage("Password must contain at least one digit.");
        }

        if (passwordOptions.RequireLowercase)
        {
            RuleFor(x => x.Password)
                .Matches(@"[a-z]")
                .WithMessage("Password must contain at least one lowercase character.");
        }

        if (passwordOptions.RequireNonAlphanumeric)
        {
            RuleFor(x => x.Password)
                .Matches(@"\W")
                .WithMessage("Password must contain at least one non alphanumeric character.");
        }

        if (passwordOptions.RequireUppercase)
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
