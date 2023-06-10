namespace RpgBooks.Modules.Identity.Application.Commands.Login;

using FluentValidation;

using RpgBooks.Modules.Identity.Domain.Settings;

/// <summary>
/// Login user command validator.
/// </summary>
public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LoginCommandValidator"/> class.
    /// </summary>
    /// <param name="options">Identity settings configuration options.</param>
    public LoginCommandValidator(IOptions<IdentitySettings> options)
    {
        var settings = options.Value;

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        this.RuleFor(u => u.Password)
            .NotEmpty();
    }
}
