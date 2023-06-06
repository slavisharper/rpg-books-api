namespace RpgBooks.Modules.Identity.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RpgBooks.Libraries.Module.Infrastructure.Persistence.Configurations;
using RpgBooks.Modules.Identity.Domain.Entities;
using RpgBooks.Modules.Identity.Domain.Validation;

internal sealed class SecurityTokenConfiguration : IEntityTypeConfiguration<SecurityToken>
{
    public void Configure(EntityTypeBuilder<SecurityToken> builder)
    {
        builder.ConfigureTableName();

        builder.Property(t => t.Value)
            .HasMaxLength(SecurityTokenValidation.Values.MaxTokenLength);

        builder.Property(t => t.SessionId)
            .HasMaxLength(SecurityTokenValidation.Values.MaxSessionIdLength);

        builder.OwnsOne(t => t.TokenType)
            .ConfigureAsEnum();
    }
}
