using AIBattle.Auras;
using AIBattle.LiveObjects.LiveComponents.Targets;
using UnityEngine;

namespace AIBattle.LiveObjects.LiveComponents.Aggressions
{
    [CreateAssetMenu(fileName = nameof(Aggression), menuName = PathStart + nameof(Aggression), order = Order)]
    public class AggressionData : LiveComponentData
    {
        [SerializeField] private AuraData _auraData;
        [SerializeField] private TargetId _targetId;

        public override LiveComponent Create(LiveObject liveObject)
        {
            Aura aura = _auraData.Create(liveObject.transform, liveObject.transform.position, (l) => l.TryGetLiveComponent(out Target target) && _targetId.Contains(target));

            return new Aggression(new()
            {
                OnEnemyAppeared = aura.OnLiveObjectEntered,
                OnEnemyDisappered = aura.OnLiveObjectExited
            });
        }
    }
}