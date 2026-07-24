using System;
using System.Collections.Generic;

namespace Core.EventBus
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, Delegate> _events = new Dictionary<Type, Delegate>();

        public static void Subscribe<T>(Action<T> listener) where T : IEvent
        {
            Type eventType = typeof(T);
            if (_events.ContainsKey(eventType))
            {
                _events[eventType] = Delegate.Combine(_events[eventType], listener);
            }
            else
            {
                _events[eventType] = listener;
            }
        }

        public static void Unsubscribe<T>(Action<T> listener) where T : IEvent
        {
            Type eventType = typeof(T);
            if (_events.ContainsKey(eventType))
            {
                _events[eventType] = Delegate.Remove(_events[eventType], listener);
                if (_events[eventType] == null)
                {
                    _events.Remove(eventType);
                }
            }
        }

        public static void Fire<T>(T eventData) where T : IEvent
        {
            Type eventType = typeof(T);
            if (_events.TryGetValue(eventType, out Delegate del))
            {
                (del as Action<T>)?.Invoke(eventData);
            }
        }

        public static void Clear()
        {
            _events.Clear();
        }
    }
}