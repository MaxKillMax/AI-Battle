using System;
using System.Collections.Generic;
using System.Linq;
using AIBattle.Events;
using AIBattle.LiveObjects.LiveComponents;
using UnityEngine;
using UnityEngine.AI;

namespace AIBattle.LiveObjects
{
    /// <summary>
    /// LiveObject is a container of LiveComponents
    /// </summary>
    [RequireComponent(typeof(Animator)), RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(Collider))]
    public partial class LiveObject : MonoBehaviour
    {
        public Event<LiveComponent> OnComponentAdded { get; private set; } = new();
        public Event<LiveComponent> OnComponentRemoved { get; private set; } = new();

        public Animator Animator { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
        public NavMeshAgent Agent { get; private set; }
        public Collider Collider { get; private set; }

        private readonly List<LiveComponent> _liveComponents = new();

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            Rigidbody = GetComponent<Rigidbody>();
            Agent = GetComponent<NavMeshAgent>();
            Collider = GetComponent<Collider>();

            OnAdded?.Invoke(this);
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _liveComponents.Count; i++)
                _liveComponents[i].OnDestroy();

            OnRemoved?.Invoke(this);
        }

        public void TryAddLiveComponent(LiveComponent liveComponent)
        {
            if (_liveComponents.Contains(liveComponent))
                return;

            _liveComponents.Add(liveComponent);
            OnComponentAdded?.Invoke(liveComponent);
        }

        public void TryRemoveLiveComponent(LiveComponent liveComponent)
        {
            if (!_liveComponents.Contains(liveComponent))
                return;

            _liveComponents.Remove(liveComponent);
            OnComponentRemoved?.Invoke(liveComponent);
        }

        public void TryRemoveLiveComponent<T>()
        {
            LiveComponent liveComponent = _liveComponents.OfType<T>().FirstOrDefault() as LiveComponent;
            TryRemoveLiveComponent(liveComponent);
        }

        public bool TryGetLiveComponent<T>(out T liveComponent) => ((liveComponent = GetLiveComponent<T>()) as object) != default;

        public T GetLiveComponent<T>() => _liveComponents.OfType<T>().FirstOrDefault();
    }

    public partial class LiveObject
    {
        private const int START_LIVEOBJECTS_CAPACITY = 10;

        private static readonly List<LiveObject> Lives = new(START_LIVEOBJECTS_CAPACITY);
        public static IEnumerable<LiveObject> LiveObjects => Lives;

        public static event Action<LiveObject> OnAdded;
        public static event Action<LiveObject> OnRemoved;

        static LiveObject()
        {
            OnAdded += (l) => Lives.Add(l);
            OnRemoved += (l) => Lives.Remove(l);
        }
    }
}
