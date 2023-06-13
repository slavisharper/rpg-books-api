namespace RpgBooks.Modules.Identity.Application.Commands.ResetPassword;

using RpgBooks.Modules.Identity.Application.Commands.Common;
using RpgBooks.Modules.Identity.Application.Resources;
using RpgBooks.Modules.Identity.Domain.Repositories;
using RpgBooks.Modules.Identity.Domain.Services.Abstractions;

internal sealed class ResetPasswordCommandHandler : BaseCommandHandler<ResetPasswordCommand>
{
    private readonly IUserDomainRepository userRepository;
    private readonly IPasswordHasher passwordHasher;
    private readonly ISecurityTokensService securityTokensService;
    private readonly IHttpUtilities httpUtilities;
    private readonly ApplicationSecrets secrets;

    public ResetPasswordCommandHandler(
        IUserDomainRepository userRepository,
        IPasswordHasher passwordHasher,
        ISecurityTokensService securityTokensService,
        IHttpUtilities httpUtilities,
        IOptions<ApplicationSecrets> secretsOptions)
    {
        this.userRepository = userRepository;
        this.passwordHasher = passwordHasher;
        this.securityTokensService = securityTokensService;
        this.httpUtilities = httpUtilities;
        this.secrets = secretsOptions.Value;
    }

    public override async Task<IAppResult> HandleCommand(ResetPasswordCommand request, CancellationToken cancellation)
    {
        var user = await this.userRepository.GetByEmailAsync(request.Email, cancellationToken: cancellation);
        if (user is null)
        {
            return this.NotFound(Messages.UserNotFound);
        }

        var userValidationResult = this.ValidateUser(user);
        if (userValidationResult is not null)
        {
            return userValidationResult;
        }

        string resetToken = this.httpUtilities.UrlDecode(request.ResetToken)!;
        if (await this.securityTokensService.DisproveResetPasswordToken(user.Id, resetToken,cancellation))
        {
            return this.ValidationFailed(Messages.InvalidResetPasswordToken);
        }

        user!.SetPasswordHash(this.passwordHasher.HashPassword(request.Password));
        await this.userRepository.SaveAsync(cancellation);

        return this.Success(Messages.PasswordChanged);
    }
}
