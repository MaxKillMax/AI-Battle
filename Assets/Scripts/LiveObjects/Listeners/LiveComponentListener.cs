using System;
using System.Linq;
using AIBattle.LiveObjects.LiveComponents;
using UnityEngine;

namespace AIBattle.LiveObjects.Listeners
{
    public abstract class LiveComponentListener<T> : MonoBehaviour where T : LiveComponent
    {
        private void Awake()
        {
            LiveObject.OnAdded += CheckLiveObject;
            LiveObject.OnRemoved += CheckLiveObject;
        }

        private void CheckLiveObject(LiveObject liveObject)
        {
            if (liveObject == null)
                return;

            liveObject.OnComponentAdded.AddListener((c) => TrySetListenState(true, liveObject, c));
            liveObject.OnComponentRemoved.AddListener((c) => TrySetListenState(false, liveObject, c));

            if (!liveObject.TryGetLiveComponent(out T component))
                return;

            SetListenState(LiveObject.LiveObjects.Contains(liveObject), liveObject, component);
        }

        private void TrySetListenState(bool state, LiveObject liveObject, LiveComponent component)
        {
            if (component.GetType() == typeof(T))
                SetListenState(state, liveObject, (T)component);
        }

        protected abstract void SetListenState(bool state, LiveObject liveObject, T component);
    }
}
