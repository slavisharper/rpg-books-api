namespace RpgBooks.Modules.Identity.Application.Commands.ConfirmEmail;

using FluentValidation;

/// <summary>
/// Validator for <see cref="ConfirmEmailCommand"/>.
/// </summary>
public sealed class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConfirmEmailCommandValidator"/> class.
    /// </summary>
    public ConfirmEmailCommandValidator()
    {
        this.RuleFor(x => x.UserId)
            .NotEmpty();

        this.RuleFor(x => x.Token)
            .NotEmpty();
    }
}
