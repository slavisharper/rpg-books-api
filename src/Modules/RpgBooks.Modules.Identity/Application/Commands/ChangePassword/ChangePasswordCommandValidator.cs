namespace RpgBooks.Modules.Identity.Application.Commands.ChangePassword;

using FluentValidation;

using Microsoft.Extensions.Options;

using RpgBooks.Modules.Identity.Application.Commands.Common;
using RpgBooks.Modules.Identity.Domain.Settings;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
