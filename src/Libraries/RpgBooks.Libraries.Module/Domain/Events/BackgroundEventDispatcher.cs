namespace RpgBooks.Libraries.Module.Domain.Events;

using Microsoft.Extensions.DependencyInjection;

using System.Threading.Tasks;

/// <summary>
/// Domain event dispatcher that dispatches events in a background application process.
/// </summary>
public sealed class BackgroundEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceScopeFactory serviceScopeFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="BackgroundEventDispatcher"/> class.
    /// </summary>
    /// <param name="serviceScopeFactory">Service scope factory.</param>
    public BackgroundEventDispatcher(IServiceScopeFactory serviceScopeFactory)
    {
        this.serviceScopeFactory = serviceScopeFactory;
    }

    /// <summary>
    /// Dispatches a domain event in a background application process.
    /// </summary>
    /// <typeparam name="TEvent">Type of the domain event.</typeparam>
    /// <param name="domainEvent">Domain event instance.</param>
    /// <param name="dispatchBehaviour">Defines the dispatch behavior.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task Dispatch<TEvent>(TEvent domainEvent, DispatchBehaviour dispatchBehaviour = DispatchBehaviour.InSequence)
        where TEvent : IDomainEvent
    {
        var dispatchInBackground = async (IServiceScopeFactory serviceScopeFactory, Type eventType, DispatchBehaviour behaviour) =>
        {
            using var serviceScope = serviceScopeFactory.CreateScope();

            var handlers = AsyncEventDispatcher.GetEventHandlers(domainEvent, eventType, serviceScope.ServiceProvider);
            if (dispatchBehaviour == DispatchBehaviour.InParallel)
            {
                await AsyncEventDispatcher.DispatchHandlersInParallel(domainEvent, eventType, handlers);
                return;
            }

            await AsyncEventDispatcher.DispatchlHandlersInSequence(domainEvent, eventType, handlers);
        };

        var eventType = domainEvent.GetType();
        dispatchInBackground.Invoke(this.serviceScopeFactory, eventType, dispatchBehaviour);
        return Task.CompletedTask;
    }
}
