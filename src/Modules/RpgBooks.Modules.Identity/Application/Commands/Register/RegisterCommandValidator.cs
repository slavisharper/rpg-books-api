namespace RpgBooks.Modules.Identity.Application.Commands.Register;

using FluentValidation;

using RpgBooks.Modules.Identity.Application.Resources;
using RpgBooks.Modules.Identity.Domain.Repositories;
using RpgBooks.Modules.Identity.Domain.Settings;

using RpgBooks.Modules.Identity.Application.Commands.Common;

/// <summary>
/// Register command request validator.
/// </summary>
public sealed class RegisterCommandValidator : PasswordValidator<RegisterCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RegisterCommandValidator"/> class.
    /// </summary>
    /// <param name="userRepository">User domain repository.</param>
    /// <param name="settingsOptions">Password strength settings.</param>
    public RegisterCommandValidator(
        IUserDomainRepository userRepository,
        IOptions<PasswordStrengthSettings> settingsOptions)
        : base(settingsOptions.Value)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Email)
            .MustAsync(async (email, cancellation) => !(await userRepository.Exists(email, cancellation)))
            .WithMessage(Messages.EmailTakenError);
    }
}
