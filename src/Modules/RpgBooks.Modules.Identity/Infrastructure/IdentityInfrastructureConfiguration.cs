namespace RpgBooks.Modules.Identity.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

using RpgBooks.Libraries.Module.Infrastructure;
using RpgBooks.Modules.Identity.Domain.Repositories;
using RpgBooks.Modules.Identity.Infrastructure.Persistence;
using RpgBooks.Modules.Identity.Infrastructure.Persistence.Repositories;

using System;

/// <summary>
/// IDentity infrastructure layer configuration.
/// </summary>
internal static class IdentityInfrastructureConfiguration
{
    /// <summary>
    /// Adds the identity infrastructure layer.
    /// </summary>
    /// <param name="services">Application services.</param>
    /// <param name="serviceProvider">Service provider.</param>
    /// <returns></returns>
    public static IServiceCollection AddIdentityInfrastructureLayer(this IServiceCollection services, IServiceProvider serviceProvider)
        => services
            .AddInfrastructure<IdentityDbContext>(serviceProvider, typeof(IdentityInfrastructureConfiguration).Assembly)
            .AddScoped<IUserDomainRepository, UserDomainRepository>();
}
