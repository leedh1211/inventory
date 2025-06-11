using System;
using System.Collections.Generic;

public static class EventBus
{
    private static readonly Dictionary<Type, Delegate> eventDict = new();

    public static void Subscribe<T>(Action<T> handler)
    {
        if (eventDict.TryGetValue(typeof(T), out var existingHandler))
        {
            eventDict[typeof(T)] = Delegate.Combine(existingHandler, handler);
        }
        else
        {
            eventDict[typeof(T)] = handler;
        }
    }

    public static void Unsubscribe<T>(Action<T> handler)
    {
        if (eventDict.TryGetValue(typeof(T), out var existingHandler))
        {
            var newHandler = Delegate.Remove(existingHandler, handler);
            if (newHandler == null) eventDict.Remove(typeof(T));
            else eventDict[typeof(T)] = newHandler;
        }
    }

    public static void Raise<T>(T eventData)
    {
        if (eventDict.TryGetValue(typeof(T), out var handler))
        {
            ((Action<T>)handler)?.Invoke(eventData);
        }
    }

    public static void ClearAll() => eventDict.Clear(); // 옵션
}