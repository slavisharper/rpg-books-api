namespace RpgBooks.Modules.Identity.Infrastructure.Persistence.Configurations;

using Cysharp.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RpgBooks.Libraries.Module.Infrastructure.Persistence.Configurations;
using RpgBooks.Modules.Identity.Domain.Entities;

using static RpgBooks.Modules.Identity.Domain.Validation.UserValidation;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ConfigureTableName();

        builder
            .Property(u => u.ConcurrencyStamp)
            .IsRowVersion();

        builder
            .OwnsOne(u => u.Email, email =>
            {
                email.WithOwner();
                email.Property(u => u.Value)
                   .HasMaxLength(ValidationConstants.Email.MaxLength);
            });

        builder
            .Property(u => u.FirstName)
            .HasMaxLength(Values.MaxNameLength);

        builder
            .Property(u => u.LastName)
            .HasMaxLength(Values.MaxNameLength);

        builder
            .Property(u => u.HonorificTitle)
            .HasMaxLength(Values.MaxTitleLength);

        builder
            .Property(u => u.MiddleName)
            .HasMaxLength(Values.MaxNameLength);

        builder
            .Property(u => u.PasswordHash)
            .HasMaxLength(Values.MaxPasswordHashLength);

        builder
            .OwnsOne(u => u.PhoneNumber, cfg =>
            {
                cfg.ConfigurePhoneNumber();
            });

        builder
            .Property(u => u.SecurityStamp)
            .HasMaxLength(Values.MaxStampLength);

        builder
            .HasMany(u => u.Claims)
            .WithOne();

        builder
            .HasMany(u => u.Roles)
            .WithMany()
            .UsingEntity(e => e.ConfigureTableName(ZString.Format("{0}s{1}", nameof(User), nameof(Role))));

        builder
            .HasMany(u => u.SecurityTokens)
            .WithOne(t => t.User);
    }
}
