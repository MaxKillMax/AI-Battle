using AIBattle.Auras;
using AIBattle.LiveObjects;
using AIBattle.LiveObjects.LiveComponents.Targets;
using UnityEngine;

namespace AIBattle.Worlds
{
    public class VictoryPoint : MonoBehaviour
    {
        public Events.Event<LiveObject> OnTargetAppeared => _visionAura.OnLiveObjectEntered;
        public Events.Event<LiveObject> OnTargetDisappeared => _visionAura.OnLiveObjectExited;
        public Events.Event<LiveObject> OnTargetWon => _victoryAura.OnLiveObjectEntered;

        [SerializeField] private Aura _victoryAura;
        [SerializeField] private Aura _visionAura;
        [SerializeField] private TargetId _victoryTarget;

        private void Awake()
        {
            _victoryAura.SetCondition(IsTarget);
            _visionAura.SetCondition(IsTarget);

            bool IsTarget(LiveObject liveObject) => liveObject.TryGetLiveComponent(out Target target) && _victoryTarget.Contains(target);
        }
    }
}
