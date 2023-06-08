namespace RpgBooks.Modules.Identity.Infrastructure.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;

using RpgBooks.Libraries.Module.Infrastructure.Persistence.Repositories;
using RpgBooks.Modules.Identity.Domain.Entities;
using RpgBooks.Modules.Identity.Domain.Repositories;
using RpgBooks.Modules.Identity.Infrastructure.Persistence;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// User domain repository.
/// </summary>
internal class UserDomainRepository : BaseDomainRepository<IdentityDbContext, User, int>, IUserDomainRepository
{
    public UserDomainRepository(IdentityDbContext context)
        : base(context)
    {
    }

    /// <inheritdoc/>
    public async Task<bool> Exists(string email, CancellationToken cancellation = default)
        => await this.context.Users.AnyAsync(u => u.Email.Value == email, cancellation);

    /// <inheritdoc/>
    public async Task<User?> GetByEmailAsync(
        string email,
        Func<IQueryable<User>, IQueryable<User>>? include = null,
        CancellationToken cancellation = default)
    {
        var query = this.context.Users.AsQueryable();
        if (include is not null)
        {
            query = include.Invoke(query);
        }

        return await query.FirstOrDefaultAsync(u => u.Email.Value == email, cancellation);
    }

    /// <inheritdoc/>
    public async Task<User?> GetByIdAsync(
        int id,
        Func<IQueryable<User>, IQueryable<User>>? include = null,
        CancellationToken cancellation = default)
    {
        var query = this.context.Users.AsQueryable();
        if (include is not null)
        {
            query = include.Invoke(query);
        }

        return await query.FirstOrDefaultAsync(u => u.Id == id, cancellation);
    }

    /// <inheritdoc/>
    public async Task<Role?> GetUserRole(string roleName, CancellationToken cancellation = default)
        => await this.context.Roles.FirstOrDefaultAsync(u => u.Name == roleName, cancellation);
}
