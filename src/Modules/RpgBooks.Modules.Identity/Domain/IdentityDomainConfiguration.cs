namespace RpgBooks.Modules.Identity.Domain;

using Cysharp.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RpgBooks.Libraries.Module.Domain;
using RpgBooks.Modules.Identity.Domain.Builders;
using RpgBooks.Modules.Identity.Domain.Builders.Abstractions;
using RpgBooks.Modules.Identity.Domain.Services;
using RpgBooks.Modules.Identity.Domain.Services.Abstractions;
using RpgBooks.Modules.Identity.Domain.Settings;

/// <summary>
/// Identity module's domain layer configuration.
/// </summary>
internal static class IdentityDomainConfiguration
{
    /// <summary>
    /// Extension method for adding the Identity domain layer.
    /// </summary>
    /// <param name="services">Application services.</param>
    /// <param name="configuration">Application configuration.</param>
    /// <returns>Configured application services.</returns>
    public static IServiceCollection AddIdentityDomainLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDomainEvents(typeof(IdentityDomainConfiguration).Assembly)
            .Configure<IdentitySettings>(configuration.GetSection(nameof(IdentitySettings)))
            .Configure<PasswordStrengthSettings>(
                configuration.GetSection(
                    ZString.Format("{0}:{1}", nameof(IdentitySettings), nameof(IdentitySettings.PasswordStrengthSettings))))
            .Configure<PasswordSecuritySettings>(
                configuration.GetSection(
                    ZString.Format("{0}:{1}", nameof(IdentitySettings), nameof(IdentitySettings.PasswordSecuritySettings))))
            .Configure<LoginSettings>(
                configuration.GetSection(
                    ZString.Format("{0}:{1}", nameof(IdentitySettings), nameof(IdentitySettings.LoginSettings))))
            .Configure<SecurityTokenSettings>(
                configuration.GetSection(
                    ZString.Format("{0}:{1}", nameof(IdentitySettings), nameof(IdentitySettings.SecurityTokenSettings))))
            .AddScoped<IPasswordHasher, PasswordHasher>()
            .AddScoped<IUserBuilder, UserBuilder>()
            .AddScoped<ISecurityTokensService, SecurityTokensService>()
            .AddScoped<IUserRoleManager, UserRoleManager>();

        return services;
    }
}
