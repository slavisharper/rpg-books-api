namespace RpgBooks.Modules.Identity.Application.Commands.RefreshToken;

using FluentValidation;

using RpgBooks.Modules.Identity.Domain.Validation;

/// <summary>
/// Refresh authentication token command validator.
/// </summary>
public sealed class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshTokenCommandValidator"/> class.
    /// </summary>
    public RefreshTokenCommandValidator()
    {
        this.RuleFor(x => x.AuthenticationToken)
            .MaximumLength(SecurityTokenValidation.Values.MaxTokenLength)
            .NotEmpty();

        this.RuleFor(x => x.RefreshToken)
            .MaximumLength(SecurityTokenValidation.Values.MaxTokenLength)
            .NotEmpty();
    }
}
