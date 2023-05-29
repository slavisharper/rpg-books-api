namespace RpgBooks.Libraries.Module.Domain.Events;

using Microsoft.Extensions.DependencyInjection;

using System.Threading.Tasks;

/// <summary>
/// Event dispatcher that dispatches events asynchronously.
/// </summary>
public sealed class AsyncEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncEventDispatcher"/> class.
    /// </summary>
    /// <param name="serviceProvider">Service provider.</param>
    public AsyncEventDispatcher(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public async Task Dispatch<TEvent>(TEvent domainEvent, DispatchBehaviour dispatchBehaviour = DispatchBehaviour.InSequence)
        where TEvent : IDomainEvent
    {
        var eventType = domainEvent.GetType();
        var handlers = GetEventHandlers(domainEvent, eventType, this.serviceProvider);

        if (dispatchBehaviour == DispatchBehaviour.InParallel)
        {
            await DispatchHandlersInParallel(domainEvent, eventType, handlers);
            return;
        }

        await DispatchlHandlersInSequence(domainEvent, eventType, handlers);
    }

    internal static IEnumerable<object?> GetEventHandlers<TEvent>(TEvent domainEvent, Type eventType, IServiceProvider serviceProvider)
        where TEvent : IDomainEvent
    {
        var eventHandlerType = typeof(IDomainEventHandler<>).MakeGenericType(eventType);
        var handlers = serviceProvider.GetServices(eventHandlerType);
        return handlers;
    }

    internal static async Task DispatchHandlersInParallel<TEvent>(TEvent domainEvent, Type eventType, IEnumerable<object?>? handlers)
        where TEvent : IDomainEvent
    {
        if (handlers is null)
        {
            return;
        }

        var tasks = handlers.Select(h => HandlerDelegatesCache.CallHandlerDelegate(domainEvent, eventType, h));
        await Task.WhenAll(tasks);
    }

    internal static async Task DispatchlHandlersInSequence<TEvent>(TEvent domainEvent, Type eventType, IEnumerable<object?>? handlers)
        where TEvent : IDomainEvent
    {
        if (handlers is null)
        {
            return;
        }

        foreach (var handler in handlers)
        {
            await HandlerDelegatesCache.CallHandlerDelegate(domainEvent, eventType, handler);
        }
    }
}
