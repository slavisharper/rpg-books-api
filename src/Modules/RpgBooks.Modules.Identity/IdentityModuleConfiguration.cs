namespace RpgBooks.Modules.Identity;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;

using RpgBooks.Libraries.Module.Application;
using RpgBooks.Libraries.Module.Application.Settings;
using RpgBooks.Libraries.Module.Infrastructure.Services.CurrentUser;
using RpgBooks.Modules.Identity.Application;
using RpgBooks.Modules.Identity.Domain;
using RpgBooks.Modules.Identity.Domain.Services.Jwt;
using RpgBooks.Modules.Identity.Domain.Settings;
using RpgBooks.Modules.Identity.Infrastructure;

using System.Text;

/// <summary>
/// Identity module configuration.
/// </summary>
public static class IdentityModuleConfiguration
{
    /// <summary>
    /// Add identity module layers.
    /// </summary>
    /// <param name="appBuilder">Web application builder.</param>
    /// <returns>Configured web application builder.</returns>
    public static WebApplicationBuilder AddAuthFeature(this WebApplicationBuilder appBuilder)
    {
        appBuilder.Services
            .AddIdentityDomainLayer(appBuilder.Configuration)
            .AddIdentityApplicationLayer();

        using var serviceScope = appBuilder.Services.GetNewServiceScope();

        appBuilder.Services
            .AddIdentityInfrastructureLayer(serviceScope.ServiceProvider);

        return appBuilder;
    }

    /// <summary>
    /// Add application authentication services.
    /// </summary>
    /// <param name="services">Application services.</param>
    /// <param name="configuration">Application configuration.</param>
    /// <returns>Configured application services.</returns>
    public static IServiceCollection AddAppAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        byte[] secret = Encoding.ASCII.GetBytes(configuration
            .GetSection(nameof(ApplicationSecrets))
            .GetValue<string>(nameof(ApplicationSecrets.AuthenticationSecret))!);

        var loginSection = configuration
            .GetSection(nameof(IdentitySettings))
            .GetSection(nameof(IdentitySettings.LoginSettings));

        string validIssuer = loginSection.GetValue<string>(nameof(IdentitySettings.LoginSettings.ValidIssuer))!;
        string validAudience = loginSection.GetValue<string>(nameof(IdentitySettings.LoginSettings.ValidAudience))!;

        services
            .AddAuthentication("Bearer")
            .AddJwtBearer(bearer =>
            {
                bearer.RequireHttpsMetadata = true;
                bearer.SaveToken = true;
                bearer.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = validIssuer,
                    ValidAudience = validAudience,
                };
            });

        int tokenExpirationInMinutes = loginSection.GetValue<int>(nameof(IdentitySettings.LoginSettings.AuthTokenTimeSpanInMinutes));
        var jwtManager = new LitJwtTokenManager(secret, validAudience, validIssuer, tokenExpirationInMinutes);
        services.TryAddSingleton<IJwtTokenManager>(jwtManager);
        services.TryAddSingleton<IJwtDecoder>(jwtManager);

        return services;
    }

    /// <summary>
    /// Add application authorization services.
    /// </summary>
    /// <param name="services">Application services.</param>
    /// <returns>Configured application services.</returns>
    public static IServiceCollection AddAppAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization();

        return services;
    }
}
