namespace RpgBooks.Modules.Identity.Application.Commands.Login;

using RpgBooks.Modules.Identity.Application.Commands.Common;
using RpgBooks.Modules.Identity.Application.Resources;
using RpgBooks.Modules.Identity.Domain.Repositories;
using RpgBooks.Modules.Identity.Domain.Services.Abstractions;
using RpgBooks.Modules.Identity.Domain.Services.Jwt;
using RpgBooks.Modules.Identity.Domain.Settings;

internal class LoginCommandHandler : BaseCommandHandler<LoginCommand, LoginResponseModel>
{
    private readonly IJwtTokenManager jwtTokenManager;
    private readonly IUserDomainRepository userRepository;
    private readonly ISecurityTokensService securityTokensService;
    private readonly IPasswordHasher passwordHasher;
    private readonly LoginSettings settings;

    public LoginCommandHandler(
        IPasswordHasher passwordHasher,
        IJwtTokenManager jwtTokenManager,
        IUserDomainRepository userRepository,
        ISecurityTokensService securityTokensService,
        IOptions<LoginSettings> loginOptions)
    {
        this.passwordHasher = passwordHasher;
        this.jwtTokenManager = jwtTokenManager;
        this.userRepository = userRepository;
        this.securityTokensService = securityTokensService;
        this.settings = loginOptions.Value;
    }

    public override async Task<IAppResult<LoginResponseModel>> HandleCommand(LoginCommand request, CancellationToken cancellation)
    {
        var user = await this.userRepository.GetByEmailAsync(
            request.Email,
            query => query.Include(u => u.Claims).Include(u => u.Roles),
            cancellation);

        if (user is null)
        {
            return this.NotFound(Messages.UserNotFound);
        }

        var userValidationResult = this.ValidateUser(user);
        if (userValidationResult is not null)
        {
            return userValidationResult;
        }

        bool isValid = this.passwordHasher.VerifyPassword(request.Password, user!.PasswordHash);
        if (!isValid)
        {
            user.RecordFailedAccess(
                this.settings.MaxLoginAttempts,
                TimeSpan.FromMinutes(this.settings.LockoutTimeSpanInMinutes));

            if (user.LockedOut)
            {
                return this.ValidationFailed(ZString.Format(Messages.LockedOut, user.LockedPeriodInMinutes));
            }

            return this.ValidationFailed(Messages.InvalidLogin);
        }

        string session = Ulid.NewUlid().ToString();
        var token = this.jwtTokenManager.GenerateToken(user, session);
        var refreshToken = await this.securityTokensService.GenerateRefreshToken(user, session, cancellation);

        await this.userRepository.SaveAsync(cancellation);

        return this.Success(Messages.LoggedIn, new LoginResponseModel(token, refreshToken!));
    }
}
