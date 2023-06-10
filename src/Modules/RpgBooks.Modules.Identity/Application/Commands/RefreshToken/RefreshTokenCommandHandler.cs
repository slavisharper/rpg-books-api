namespace RpgBooks.Modules.Identity.Application.Commands.RefreshToken;

using RpgBooks.Modules.Identity.Application.Commands.Common;
using RpgBooks.Modules.Identity.Application.Commands.Login;
using RpgBooks.Modules.Identity.Application.Resources;
using RpgBooks.Modules.Identity.Domain.Repositories;
using RpgBooks.Modules.Identity.Domain.Services.Abstractions;
using RpgBooks.Modules.Identity.Domain.Services.Jwt;

internal sealed class RefreshTokenCommandHandler : BaseCommandHandler<RefreshTokenCommand, LoginResponseModel>
{
    private readonly IJwtTokenManager jwtTokenManager;
    private readonly IUserDomainRepository userRepository;
    private readonly ISecurityTokensService securityTokensService;
    private readonly ApplicationSecrets secrets;

    public RefreshTokenCommandHandler(
        IJwtTokenManager jwtTokenManager,
        IUserDomainRepository userRepository,
        ISecurityTokensService securityTokensService,
        IOptions<ApplicationSecrets> secretsOptions)
    {
        this.jwtTokenManager = jwtTokenManager;
        this.userRepository = userRepository;
        this.securityTokensService = securityTokensService;
        this.secrets = secretsOptions.Value;
    }

    public override async Task<IAppResult<LoginResponseModel>> HandleCommand(RefreshTokenCommand request, CancellationToken cancellation)
    {
        var jwtPayload = this.jwtTokenManager.ReadToken(request.AuthenticationToken);
        if (jwtPayload is null)
        {
            return this.ValidationFailed(Messages.JwtReadFailed);
        }

        if (jwtPayload.Uid is null
            || jwtPayload.SessionId is null
            || jwtPayload.SecurityStamp is null
            || jwtPayload.Email is null)
        {
            return this.Unauthorized(Messages.NoAthorityFailure);
        }

        var user = await this.userRepository.GetByIdAsync(
            int.Parse(jwtPayload.Uid),
            q => q.Include(u => u.Roles).Include(u => u.Claims),
            cancellation);

        if (user is null)
        {
            return this.NotFound(Messages.UserNotFound);
        }

        var userValidationResult = this.ValidateUser(user, jwtPayload.SecurityStamp);
        if (userValidationResult is not null)
        {
            return userValidationResult;
        }

        if (await this.securityTokensService.DisproveRefreshToken(user.Id, request.RefreshToken, jwtPayload.SessionId, cancellation))
        {
            return this.Unauthorized(Messages.InvalidRefreshToken);
        }

        var token = this.jwtTokenManager.GenerateToken(user, jwtPayload.SessionId);
        var refreshToken = await this.securityTokensService.GenerateRefreshToken(user, jwtPayload.SessionId, cancellation);
        await this.userRepository.SaveAsync(cancellation);

        return this.Success(Messages.AuthTokenRefreshed, new LoginResponseModel(token, refreshToken));
    }
}