namespace RpgBooks.Modules.Identity.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RpgBooks.Modules.Identity.Domain.Entities;
using RpgBooks.Modules.Identity.Domain.Validation;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ConfigureTableName();

        builder.Property(r => r.Name)
            .HasMaxLength(RoleValidation.Values.MaxNameLenght);

        builder.HasMany(r => r.Claims)
            .WithOne()
            .HasForeignKey("RoleId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
