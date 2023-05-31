namespace RpgBooks.Libraries.Module.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RpgBooks.Libraries.Module.Domain.Common.ValueObjects;

/// <summary>
/// Enumeration configuration methods.
/// </summary>
public static class PhoneNumberConfiguration
{
    /// <summary>
    /// Configure enumeration for EF.
    /// </summary>
    /// <typeparam name="TOwner">Owner type.</typeparam>
    /// <param name="cfg">Navigation builder instance.</param>
    public static void ConfigurePhoneNumber<TOwner>(this OwnedNavigationBuilder<TOwner, PhoneNumber> cfg)
        where TOwner : class
    {
        cfg.WithOwner();
        cfg.Property(i => i.Value)
            .HasMaxLength(ValidationConstants.Phone.MaxLength);
    }
}
