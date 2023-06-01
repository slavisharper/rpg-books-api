namespace RpgBooks.Modules.Identity.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using RpgBooks.Libraries.Module.Application.Settings;
using RpgBooks.Libraries.Module.Infrastructure.Persistence.Abstractions;
using RpgBooks.Modules.Identity.Domain.Entities;

using System.Threading;
using System.Threading.Tasks;

internal sealed class AuthDbInitializer : IDbInitializer
{
    private readonly IdentityDbContext dbContext;
    private readonly ApplicationSettings appSettings;

    public AuthDbInitializer(IdentityDbContext dbContext, IOptions<ApplicationSettings> appSettings)
    {
        this.dbContext = dbContext;
        this.appSettings = appSettings.Value;
    }

    public void Initialize()
    {
        if (this.dbContext.Database.IsRelational())
        {
            this.dbContext.Database.Migrate();
        }

        this.SeedRoles();
    }

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        if (this.dbContext.Database.IsRelational())
        {
            await this.dbContext.Database.EnsureCreatedAsync(cancellationToken);
            await this.dbContext.Database.MigrateAsync(cancellationToken);
        }

        this.SeedRoles();
    }

    private void SeedRoles()
    {
        if (this.dbContext.Roles.Any())
        {
            return;
        }

        this.dbContext.Add(new Role(this.appSettings.AdminRoleName));
        this.dbContext.Add(new Role(this.appSettings.DeveloperRoleName));
        this.dbContext.SaveChanges();
    }
}
