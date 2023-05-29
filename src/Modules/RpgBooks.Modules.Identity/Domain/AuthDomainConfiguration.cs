namespace RpgBooks.Modules.Identity.Domain;

using Cysharp.Text;

using RpgBooks.Modules.Identity.Domain.Builders;
using RpgBooks.Modules.Identity.Domain.Builders.Abstractions;
using RpgBooks.Modules.Identity.Domain.Services;
using RpgBooks.Modules.Identity.Domain.Services.Abstractions;
using RpgBooks.Modules.Identity.Domain.Settings;
using RpgBooks.Libraries.Module.Domain;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

internal static class AuthDomainConfiguration
{
    // Configure services for domain layer. 
    public static IServiceCollection AddAuthDomain(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDomainEvents(typeof(AuthDomainConfiguration).Assembly)
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
            .AddScoped<IUserManager, UserManager>();

        return services;
    }
}
