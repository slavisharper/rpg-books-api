namespace RpgBooks.Modules.Identity.Application.Commands.RefreshToken;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using RpgBooks.Libraries.Module.Application.Commands;
using RpgBooks.Libraries.Module.Application.Commands.Extensions;
using RpgBooks.Libraries.Module.Application.Results.Contracts;
using RpgBooks.Libraries.Module.Application.Settings;
using RpgBooks.Modules.Identity.Application.Commands.Login;
using RpgBooks.Modules.Identity.Application.Resources;
using RpgBooks.Modules.Identity.Domain.Repositories;
using RpgBooks.Modules.Identity.Domain.Services.Abstractions;
using RpgBooks.Modules.Identity.Domain.Services.Jwt;

using System.Security;
using System.Threading;
using System.Threading.Tasks;

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
            q => q.Include(u => u.SecurityTokens),
            cancellation);

        if (user is null)
        {
            return this.NotFound(Messages.UserNotFound);
        }

        if (jwtPayload.SecurityStamp != user.SecurityStamp)
        {
            return this.Unauthorized(Messages.AuthorityModifiedFailure);
        }

        if (user.Blocked)
        {
            return this.Unauthorized(Messages.AccountBlocked);
        }

        var lastRefreshToken = this.securityTokensService.GetLastRefreshToken(user, jwtPayload.SessionId);
        if (lastRefreshToken is null
            || lastRefreshToken.SessionId != jwtPayload.SessionId
            || request.RefreshToken != lastRefreshToken?.Value.Decrypt(this.secrets.TokenProtectionSecret))
        {
            return this.Unauthorized(Messages.InvalidRefreshToken);
        }

        if (lastRefreshToken.ExpirationTime is not null && lastRefreshToken.ExpirationTime < DateTimeOffset.UtcNow)
        {
            return this.Unauthorized(Messages.ExpiredRefreshToken);
        }

        var token = this.jwtTokenManager.GenerateToken(user, jwtPayload.SessionId);
        var refreshToken = await this.securityTokensService.GenerateRefreshToken(user, jwtPayload.SessionId, cancellation);
        await this.userRepository.SaveAsync(cancellation);

        return this.Success(Messages.AuthTokenRefreshed, new LoginResponseModel(token, refreshToken));
    }
}