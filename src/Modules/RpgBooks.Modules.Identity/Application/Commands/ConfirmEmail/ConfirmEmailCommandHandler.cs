namespace RpgBooks.Modules.Identity.Application.Commands.ConfirmEmail;

using RpgBooks.Modules.Identity.Application.Commands.Common;
using RpgBooks.Modules.Identity.Application.Resources;
using RpgBooks.Modules.Identity.Domain.Repositories;
using RpgBooks.Modules.Identity.Domain.Services.Abstractions;

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
        var user = await this.userReopository.GetAsync(request.UserId, cancellation);
        if (user is null)
        {
            return this.NotFound(Messages.UserNotFound);
        }

        var userValidationResult = this.ValidateUser(user);
        if (userValidationResult is not null)
        {
            return userValidationResult;
        }

        if (user!.EmailConfirmed)
        {
            return this.Success(Messages.EmailAlreadyConfirmed);
        }

        var confirmationToken = this.httpUtilities.UrlDecode(request.Token);
        var lastConfirmationToken = await this.securityTokensService.GetLastEmailConfirmationToken(user.Id);
        if (lastConfirmationToken is null
            || lastConfirmationToken.IsExpired
            || confirmationToken != lastConfirmationToken.Value.Decrypt(secrets.TokenProtectionSecret))
        {
            return this.ValidationFailed(Messages.InvalidEmailConfirmationToken);
        }

        user.ConfirmEmail();
        await this.userReopository.SaveAsync();

        return this.Success(Messages.UserEmailConfirmed);
    }
}
