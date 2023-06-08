using System;
using AIBattle.Events;
using AIBattle.LiveObjects;
using UnityEngine;

namespace AIBattle.Auras
{
    public class Aura : MonoBehaviour
    {
        public Event<LiveObject> OnLiveObjectEntered { get; private set; } = new();
        public Event<LiveObject> OnLiveObjectExited { get; private set; } = new();

        private Func<LiveObject, bool> _condition;

        public void SetCondition(Func<LiveObject, bool> condition) => _condition = condition;

        public void SetScale(Vector3 scale) => transform.localScale = scale;

        private void OnTriggerEnter(Collider enteredObject) => TryInvoke(OnLiveObjectEntered, enteredObject.transform);

        private void OnCollisionEnter(Collision enteredObject) => TryInvoke(OnLiveObjectEntered, enteredObject.transform);

        private void OnTriggerExit(Collider exitedObject) => TryInvoke(OnLiveObjectExited, exitedObject.transform);

        private void OnCollisionExit(Collision exitedObject) => TryInvoke(OnLiveObjectExited, exitedObject.transform);

        private void TryInvoke(Event<LiveObject> @event, Transform transform)
        {
            if (transform.TryGetComponent(out LiveObject liveObject) && GetConditionResult(liveObject))
                @event?.Invoke(liveObject);
        }

        private bool GetConditionResult(LiveObject liveObject) => _condition == null || _condition.Invoke(liveObject);
    }
}