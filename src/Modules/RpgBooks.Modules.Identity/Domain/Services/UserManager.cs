namespace RpgBooks.Modules.Identity.Domain.Services;

using RpgBooks.Libraries.Module.Application.Settings;
using RpgBooks.Modules.Identity.Domain.Entities;
using RpgBooks.Modules.Identity.Domain.Exceptions;
using RpgBooks.Modules.Identity.Domain.Repositories;
using RpgBooks.Modules.Identity.Domain.Services.Abstractions;
using RpgBooks.Modules.Identity.Domain.Settings;

using Microsoft.Extensions.Options;

using System.Security;
using System.Threading.Tasks;

internal sealed class UserManager : IUserManager
{
    private readonly IUserDomainRepository userRepository;
    private readonly ApplicationSettings appSettings;

    public UserManager(
        IUserDomainRepository userDomainRepository,
        IOptions<ApplicationSettings> appSettings)
    {
        this.userRepository = userDomainRepository;
        this.appSettings = appSettings.Value;
    }

    public async Task AddToAdminRole(User user, CancellationToken cancellation = default)
        => await this.AddToRole(user, this.appSettings.AdminRoleName, cancellation);

    public async Task AddToDevRole(User user, CancellationToken cancellation = default)
        => await this.AddToRole(user, this.appSettings.AdminRoleName, cancellation);

    public async Task AddToRole(User user, string roleName, CancellationToken cancellation = default)
    {
        if (user.HasRole(roleName))
        {
            return;
        }

        var role = await this.userRepository.GetUserRole(this.appSettings.AdminRoleName, cancellation);
        Ensure.IsNotNull<Role, RoleNotFoundException>(role, roleName);

        user.AddRole(role!);
        await this.userRepository.SaveAsync(cancellation);
    }
}
