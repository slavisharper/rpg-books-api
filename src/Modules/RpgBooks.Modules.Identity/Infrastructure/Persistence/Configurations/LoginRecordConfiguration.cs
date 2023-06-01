namespace RpgBooks.Modules.Identity.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RpgBooks.Modules.Identity.Domain.Entities;
using RpgBooks.Modules.Identity.Domain.Validation;

internal sealed class LoginRecordConfiguration : IEntityTypeConfiguration<LoginRecord>
{
    public void Configure(EntityTypeBuilder<LoginRecord> builder)
    {
        builder.ConfigureTableName();

        builder.Property(l => l.UserAgent)
            .HasMaxLength(LoginRecordValidation.Values.MaxUserAgentLenght);

        builder.Property(l => l.IpAddress)
            .HasMaxLength(LoginRecordValidation.Values.MaxIpAddressLenght);

        builder.HasOne(l => l.User)
            .WithMany();
    }
}
