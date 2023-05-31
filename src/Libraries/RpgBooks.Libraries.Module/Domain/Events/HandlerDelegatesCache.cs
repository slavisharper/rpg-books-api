namespace RpgBooks.Libraries.Module.Domain.Events;

using System.Collections.Concurrent;
using System.Reflection;

internal static class HandlerDelegatesCache
{
    private static readonly ConcurrentDictionary<Type, Func<object, object, Task>> HandlerDelegatesDict = new();

    internal static async Task CallHandlerDelegate(IDomainEvent domainEvent, Type eventType, object? handler)
    {
        var concreteHandlerType = handler!.GetType();

        // Retrieve the `MethodInfo` of the method that defines how to expressively call the event handler's `Handle()` method.
        var handleDelegateMethod = GetMethodInfo(nameof(MakeHandleDelegate), BindingFlags.Static | BindingFlags.NonPublic)
            .MakeGenericMethod(eventType, concreteHandlerType);

        var handleDelegate = HandlerDelegatesDict.GetOrAdd(concreteHandlerType, type =>
        {
            // Create a delegate pointing to the `MakeHandleDelegate` method
            var handleDelegateInvoker = handleDelegateMethod.CreateDelegate<Func<Func<object, object, Task>>>();

            // Invokes the `MakeHandleDelegate` method, returning another delegate pointing to the `Handle()` method.
            return handleDelegateInvoker.Invoke();
        });

        // Invokes the `Handle()` method of the handler.
        await handleDelegate(domainEvent, handler);
    }

    private static Func<object, object, Task> MakeHandleDelegate<TEvent, THandler>()
        where TEvent : IDomainEvent
        where THandler : IDomainEventHandler<TEvent>
    {
        return async (evt, handler) =>
        {
            var domainEvent = (TEvent)evt;
            var domainEventHandler = (THandler)handler;

            await domainEventHandler.HandleEvent(domainEvent);
            domainEvent.MarkAsHandled();
        };
    }

    private static MethodInfo GetMethodInfo(string methodName, BindingFlags bindingFlags)
    {
        var methodInfo = typeof(HandlerDelegatesCache).GetMethod(methodName, bindingFlags);

        return methodInfo ?? throw new MissingMethodException(methodName);
    }
}
