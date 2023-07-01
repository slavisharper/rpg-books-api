namespace RpgBooks.Modules.Identity.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using RpgBooks.Libraries.Module.Application.Services.CurrentUser;
using RpgBooks.Libraries.Module.Domain.Events;
using RpgBooks.Libraries.Module.Infrastructure.Persistence;
using RpgBooks.Modules.Identity.Domain.Entities;

internal sealed class IdentityDbContext : BaseDbContext<IdentityDbContext>
{
    public IdentityDbContext(
        DbContextOptions<IdentityDbContext> options,
        ICurrentUserService currentUserService,
        IDomainEventDispatcher eventDispatcher,
        ILogger logger)
        : base(options, currentUserService, eventDispatcher, logger)
    {
    }

    public DbSet<Claim> Claims { get; set; } = default!;

    public DbSet<Role> Roles { get; set; } = default!;

    public DbSet<SecurityToken> SecurityTokens { get; set; } = default!;

    public DbSet<User> Users { get; set; } = default!;

    public DbSet<LoginRecord> LoginRecords { get; set; } = default!;
}
