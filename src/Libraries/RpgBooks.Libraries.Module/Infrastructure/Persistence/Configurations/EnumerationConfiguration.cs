namespace RpgBooks.Libraries.Module.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RpgBooks.Libraries.Module.Domain.Entities;

/// <summary>
/// Enumeration configuration methods.
/// </summary>
public static class EnumerationConfiguration
{
    /// <summary>
    /// Configure enumeration for EF.
    /// </summary>
    /// <typeparam name="TOwner">Owner type.</typeparam>
    /// <typeparam name="TDependant">Enumeration type.</typeparam>
    /// <param name="cfg">Navigation builder instance.</param>
    public static void ConfigureAsEnum<TOwner, TDependant>(this OwnedNavigationBuilder<TOwner, TDependant> cfg)
        where TOwner : class
        where TDependant : Enumeration
    {
        cfg.WithOwner();
        cfg.Property(i => i.Value);
    }
}
