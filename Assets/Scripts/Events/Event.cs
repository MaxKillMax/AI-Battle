using System;

namespace AIBattle.Events
{
    /// <summary>
    /// A convenient variation of the event
    /// </summary>
    public class Event
    {
        private event Action Action;

        public Event(Action action = null) => Action = action;

        public void AddListener(Action action) => Action += action;

        public void RemoveListener(Action action) => Action -= action;

        public void SetListenState(bool state, Action action) => Action = state ? Action + action : Action - action;

        public void RemoveAllListeners() => Action = null;

        public void Invoke() => Action?.Invoke();
    }

    /// <summary>
    /// A convenient variation of the event
    /// </summary>
    public class Event<T>
    {
        private event Action<T> Action;

        public Event(Action<T> action = null) => Action = action;

        public void AddListener(Action<T> action) => Action += action;

        public void RemoveListener(Action<T> action) => Action -= action;

        public void SetListenState(bool state, Action<T> action) => Action = state ? Action + action : Action - action;

        public void RemoveAllListener() => Action = null;

        public void Invoke(T t) => Action?.Invoke(t);
    }

    /// <summary>
    /// A convenient variation of the event
    /// </summary>
    public class Event<T, K>
    {
        private event Action<T, K> Action;

        public Event(Action<T, K> action = null) => Action = action;

        public void AddListener(Action<T, K> action) => Action += action;

        public void RemoveListener(Action<T, K> action) => Action -= action;

        public void SetListenState(bool state, Action<T, K> action) => Action = state ? Action + action : Action - action;

        public void RemoveAllListener() => Action = null;

        public void Invoke(T t, K k) => Action?.Invoke(t, k);
    }
}
