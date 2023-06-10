namespace RpgBooks.Modules.Identity.Application.Commands.Register;

using RpgBooks.Modules.Identity.Application.Resources;
using RpgBooks.Modules.Identity.Domain.Builders.Abstractions;
using RpgBooks.Modules.Identity.Domain.Repositories;
using RpgBooks.Modules.Identity.Domain.Services.Abstractions;

internal sealed class RegisterCommandHandler : BaseCommandHandler<RegisterCommand, RegisterResponseModel>
{
    private readonly IUserBuilder userBuilder;
    private readonly IUserRoleManager userManager;
    private readonly IUserDomainRepository userRepository;
    private readonly ApplicationSettings appSettings;
    private readonly DevSettings devSettings;

    public RegisterCommandHandler(
        IUserBuilder userBuilder,
        IUserRoleManager userManager,
        IUserDomainRepository userRepository,
        IOptions<ApplicationSettings> appSettings,
        IOptions<DevSettings> devSettings)
    {
        this.userBuilder = userBuilder;
        this.userManager = userManager;
        this.userRepository = userRepository;
        this.appSettings = appSettings.Value;
        this.devSettings = devSettings.Value;
    }

    public override async Task<IAppResult<RegisterResponseModel>> HandleCommand(RegisterCommand request, CancellationToken cancellation)
    {
        var user = await this.userBuilder
            .WithEmail(request.Email)
            .WithPassword(request.Password)
            .Build();

        bool isDeveloper = this.devSettings.TeamEmails is not null 
            && this.devSettings.TeamEmails.Contains(request.Email);
        if (isDeveloper)
        {
            await this.userManager.AddToDevRole(user, cancellation);
        }

        bool isAdmin = this.appSettings.AdminEmails is not null 
            && this.appSettings.AdminEmails.Contains(request.Email);
        if (isDeveloper || isAdmin)
        {
            await this.userManager.AddToAdminRole(user, cancellation);
        }

        await this.userRepository.AddAsync(user, cancellation);
        await this.userRepository.SaveAsync(cancellation);

        return this.Success(Messages.RegisterSuccess, new RegisterResponseModel(user.Id));
    }
}
