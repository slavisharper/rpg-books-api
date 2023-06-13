namespace RpgBooks.Modules.Identity.Application.Commands.UpdateDetails;

using FluentValidation;
using static RpgBooks.Modules.Identity.Domain.Validation.UserValidation.Values;

/// <summary>
/// Update details command validator.
/// </summary>
public sealed class UpdateDetailsComandValidator : AbstractValidator<UpdateDetailsComand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateDetailsComandValidator"/> class.
    /// </summary>
    public UpdateDetailsComandValidator()
    {
        this.RuleFor(x => x.HonorificTitle)
            .MaximumLength(MaxTitleLength);

        this.RuleFor(x => x.FirstName)
            .MaximumLength(MaxNameLength);

        this.RuleFor(x => x.MiddleName)
            .MaximumLength(MaxNameLength);

        this.RuleFor(x => x.LastName)
            .MaximumLength(MaxNameLength);

        this.RuleFor(x => x.PhoneNumber)
            .MinimumLength(ValidationConstants.Phone.MinLength)
            .MaximumLength(ValidationConstants.Phone.MaxLength);
    }
}
