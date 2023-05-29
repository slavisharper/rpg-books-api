namespace RpgBooks.Modules.Identity.Domain.Services.Abstractions;

using RpgBooks.Modules.Identity.Domain.Entities;

internal interface IUserManager
{
    Task AddToRole(User user, string roleName, CancellationToken cancellation = default);

    Task AddToAdminRole(User user, CancellationToken cancellation = default);

    Task AddToDevRole(User user, CancellationToken cancellation = default);
}
