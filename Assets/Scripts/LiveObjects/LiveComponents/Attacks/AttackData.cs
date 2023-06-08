using AIBattle.Auras;
using AIBattle.LiveObjects.LiveComponents.Healths;
using AIBattle.LiveObjects.LiveComponents.Targets;
using UnityEngine;

namespace AIBattle.LiveObjects.LiveComponents.Attacks
{
    [CreateAssetMenu(fileName = nameof(Attack), menuName = PathStart + nameof(Attack), order = Order)]
    public class AttackData : LiveComponentData
    {
        [SerializeField] private AuraData _auraData;
        [SerializeField] private TargetId _enemyId;
        [SerializeField] private HealthAction _action;
        [SerializeField] private Animators.Animation _animation;

        public override LiveComponent Create(LiveObject liveObject)
        {
            Aura aura = _auraData.Create(liveObject.transform, liveObject.transform.position, (l) => l.TryGetLiveComponent(out Health health) && l.TryGetLiveComponent(out Target target) && _enemyId.Contains(target));

            Events.Event onAttackCompleted = new();
            Animators.Animation animation = _animation;
            animation.OnExited = () => onAttackCompleted?.Invoke();

            return new Attack(new()
            {
                OnAttackStarted = new(() => liveObject.GetLiveComponent<Animators.Animator>().Animate(animation)),
                OnAttackCompleted = onAttackCompleted,
                OnEnemyAppeared = aura.OnLiveObjectEntered,
                OnEnemyDisappered = aura.OnLiveObjectExited,
                Action = _action
            });
        }
    }
}