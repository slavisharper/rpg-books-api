namespace RpgBooks.Modules.Identity.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RpgBooks.Modules.Identity.Domain.Entities;
using RpgBooks.Modules.Identity.Domain.Validation;

internal sealed class ClaimConfiguration : IEntityTypeConfiguration<Claim>
{
    public void Configure(EntityTypeBuilder<Claim> builder)
    {
        builder.ConfigureTableName();

        builder.Property(c => c.Type)
            .HasMaxLength(ClaimValidation.Values.MaxTypeLenght)
            .IsRequired();

        builder.Property(c => c.Value)
            .HasMaxLength(ClaimValidation.Values.MaxValueLenght)
            .IsRequired();

        builder.Property(c => c.ValueType)
            .HasMaxLength(ClaimValidation.Values.MaxValueTypeLenght)
            .IsRequired();
    }
}
