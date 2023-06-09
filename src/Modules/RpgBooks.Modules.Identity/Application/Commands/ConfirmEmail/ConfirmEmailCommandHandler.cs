﻿namespace RpgBooks.Modules.Identity.Application.Commands.ConfirmEmail;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using RpgBooks.Libraries.Module.Application.Commands;
using RpgBooks.Libraries.Module.Application.Commands.Extensions;
using RpgBooks.Libraries.Module.Application.Results.Contracts;
using RpgBooks.Libraries.Module.Application.Services;
using RpgBooks.Libraries.Module.Application.Settings;
using RpgBooks.Modules.Identity.Application.Resources;
using RpgBooks.Modules.Identity.Domain.Repositories;
using RpgBooks.Modules.Identity.Domain.Services.Abstractions;

using System.Security;
using System.Threading;
using System.Threading.Tasks;

internal sealed class ConfirmEmailCommandHandler : BaseCommandHandler<ConfirmEmailCommand>
{
    private readonly IUserDomainRepository userReopository;
    private readonly ISecurityTokensService securityTokensService;
    private readonly IHttpUtilities httpUtilities;
    private readonly ApplicationSecrets secrets;

    public ConfirmEmailCommandHandler(
        IUserDomainRepository userReopository,
        ISecurityTokensService securityTokensService,
        IHttpUtilities httpUtilities,
        IOptions<ApplicationSecrets> secretsOptions)
    {
        this.userReopository = userReopository;
        this.securityTokensService = securityTokensService;
        this.httpUtilities = httpUtilities;
        this.secrets = secretsOptions.Value;
    }

    public override async Task<IAppResult> HandleCommand(ConfirmEmailCommand request, CancellationToken cancellation)
    {
        var user = await this.userReopository.GetByIdAsync(request.UserId, q => q.Include(u => u.SecurityTokens), cancellation);
        if (user is null)
        {
            return this.NotFound(Messages.UserNotFound);
        }

        if (user.EmailConfirmed)
        {
            return this.Success(Messages.EmailAlreadyConfirmed);
        }

        var confirmationToken = this.httpUtilities.UrlDecode(request.Token);
        var lastConfirmationToken = this.securityTokensService.GetLastEmailConfirmationToken(user);
        if (lastConfirmationToken is null
            || confirmationToken != lastConfirmationToken.Value.Decrypt(secrets.TokenProtectionSecret))
        {
            return this.ValidationFailed(Messages.InvalidEmailConfirmationToken);
        }

        user.ConfirmEmail();
        await this.userReopository.SaveAsync();

        return this.Success(Messages.UserEmailConfirmed);
    }
}
