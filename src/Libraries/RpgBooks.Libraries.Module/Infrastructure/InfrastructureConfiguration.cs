namespace RpgBooks.Libraries.Module.Infrastructure;

using Cysharp.Text;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

using RpgBooks.Libraries.Module.Application.Commands.Contracts;
using RpgBooks.Libraries.Module.Application.Queries.Contracts;
using RpgBooks.Libraries.Module.Application.Resources.Email;
using RpgBooks.Libraries.Module.Application.Services;
using RpgBooks.Libraries.Module.Application.Services.CurrentUser;
using RpgBooks.Libraries.Module.Application.Services.Dev;
using RpgBooks.Libraries.Module.Application.Services.Email;
using RpgBooks.Libraries.Module.Application.Settings;
using RpgBooks.Libraries.Module.Domain.Repositories;
using RpgBooks.Libraries.Module.Infrastructure.Persistence.Abstractions;
using RpgBooks.Libraries.Module.Infrastructure.Services;
using RpgBooks.Libraries.Module.Infrastructure.Services.CurrentUser;
using RpgBooks.Libraries.Module.Infrastructure.Services.Dev;
using RpgBooks.Libraries.Module.Infrastructure.Services.Email;
using RpgBooks.Libraries.Templates;


using SendGrid.Extensions.DependencyInjection;

using System.Reflection;

/// <summary>
/// Base module infrastructure configuration.
/// </summary>
public static class InfrastructureConfiguration
{
    /// <summary>
    /// Add infrastructure services per module.
    /// </summary>
    /// <typeparam name="TDbContext">Type of the main database context used in the module.</typeparam>
    /// <param name="services">Application services collection.</param>
    /// <param name="serviceProvider"></param>
    /// <param name="assemblies">Assemblies containing DB initializers.</param>
    /// <returns>Configured services collection.</returns>
    public static IServiceCollection AddInfrastructure<TDbContext>(
        this IServiceCollection services,
        IServiceProvider serviceProvider,
        params Assembly[] assemblies)
            where TDbContext : DbContext, IDbContext
    {
        var secrets = serviceProvider
            .GetRequiredService<IOptions<ApplicationSecrets>>()
            .Value;

        var appSettings = serviceProvider
            .GetRequiredService<IOptions<ApplicationSettings>>()
            .Value;

        services
            .AddDatabase<TDbContext>(secrets.DefaultConnectionString)
            .AddDbInitializers(assemblies);

        return services;
    }

    /// <summary>
    /// Add common required services. This should be registered once per application.
    /// </summary>
    /// <param name="services">Application services collection.</param>
    /// <param name="configuration">Application configuration.</param>
    /// <param name="emailTemplatesAssemblies">Assemblies containing email templates.</param>
    /// <returns>Configured services collection.</returns>
    public static IServiceCollection AddCommonRequiredServices(
        this IServiceCollection services,
        IConfiguration configuration,
        params Assembly[] emailTemplatesAssemblies)
    {
        string sendGridApiKey = configuration
            .GetValue<string>(ZString.Format("{0}:{1}", nameof(ApplicationSecrets), nameof(ApplicationSecrets.SendGridSecretKey)))!;

        services
            .AddScoped<ICurrentUserService, CurrentUserService>()
            .AddSingleton<IEmailSender, SendGridEmailSender>()
            .AddSingleton<IDevTeamNotificationService, DevTeamEmailNotificationService>()
            .AddSingleton<IUrlProvider, UrlProvider>()
            .RegisterRenderer<IEmailTemplateRenderer, EmailFluidTemplateRenderer>(settings =>
            {
                settings.SetDefaultLayoutModel(new LayoutModel());
                settings.SetTemplateAssemblies(emailTemplatesAssemblies);
            })
            .AddSendGrid(options =>
            {
                options.ApiKey = sendGridApiKey;
            });

        return services;
    }

    /// <summary>
    /// Add database configuration. This should be registered once per context.
    /// </summary>
    /// <typeparam name="TDbContext">Type of the database context.</typeparam>
    /// <param name="services">Application services collection.</param>
    /// <param name="connectionString">Database connection string.</param>
    /// <returns>Configured services collection.</returns>
    public static IServiceCollection AddDatabase<TDbContext>(this IServiceCollection services, string connectionString)
        where TDbContext : DbContext, IDbContext
    {
        var dbAssembly = typeof(TDbContext).Assembly;

        services
            .AddScoped<SqlConnection>(provider => new SqlConnection(connectionString));

        services
            .AddDbContext<TDbContext>(options =>
            {
                options
                    .UseSqlServer(connectionString, sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(dbAssembly.FullName);
                    });
            });

        services
            .Scan(scan => scan
                .FromAssemblies(dbAssembly)
                .AddClasses(c => c.AssignableTo(typeof(IReadOnlyRepository)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        return services;
    }

    private static IServiceCollection AddDbInitializers(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        services
            .Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IDomainRepository<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services
            .Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo<IDbInitializer>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}
