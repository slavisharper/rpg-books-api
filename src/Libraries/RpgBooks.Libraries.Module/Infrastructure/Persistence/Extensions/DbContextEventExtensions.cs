namespace RpgBooks.Libraries.Module.Infrastructure.Persistence.Extensions;

using Microsoft.Extensions.Logging;

using RpgBooks.Libraries.Module.Domain.Entities.Abstractions;
using RpgBooks.Libraries.Module.Domain.Events;
using RpgBooks.Libraries.Module.Infrastructure.Persistence.Abstractions;

using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

/// <summary>
/// Domain event extensions for <see cref="IDbContext"/>.
/// </summary>
public static class DbContextEventExtensions
{
    /// <summary>
    /// Dispatch domain events of all entities that are modified.
    /// </summary>
    /// <param name="context">DB Context.</param>
    /// <param name="eventDispatcher">Custom event dispatcher.</param>
    /// <param name="logger">Logger instance.</param>
    /// <returns>Performed task.</returns>
    public static async Task DispatchEvents(
        this IDbContext context,
        IDomainEventDispatcher eventDispatcher,
        ILogger<IDbContext> logger)
    {
        var entities = context.ChangeTracker
            .Entries<IBaseEntity>()
            .Select(e => e.Entity)
            .Where(e => e.Events.Any())
            .ToArray();

        foreach (var entity in entities)
        {
            foreach (var domainEvent in entity.Events)
            {
                if (domainEvent.Published)
                {
                    continue;
                }

                domainEvent.MarkAsPublished();
                if (domainEvent.Poisoned)
                {
                    logger.LogError("Event is poisoned and needs a review! {Event}", JsonSerializer.Serialize(domainEvent));
                    continue;
                }

                await eventDispatcher.Dispatch(domainEvent);
                domainEvent.MarkAsHandled();
            }

            entity.ClearEvents();
        }
    }
}
