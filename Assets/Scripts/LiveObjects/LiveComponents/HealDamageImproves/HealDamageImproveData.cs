using AIBattle.LiveObjects.LiveComponents.Attacks;
using AIBattle.LiveObjects.LiveComponents.Healths;
using UnityEngine;

namespace AIBattle.LiveObjects.LiveComponents.HealDamageImproves
{
    [CreateAssetMenu(fileName = nameof(HealDamageImprove), menuName = PathStart + nameof(HealDamageImprove), order = Order)]
    public class HealDamageImproveData : LiveComponentData
    {
        [SerializeField, Range(0, 60)] private float _delay = 30;

        [SerializeField] private HealthAction _heal;
        [SerializeField] private HealthAction _damageUp;

        public override LiveComponent Create(LiveObject liveObject) => new HealDamageImprove(new()
        {
            Attack = liveObject.GetLiveComponent<Attack>(),
            Health = liveObject.GetLiveComponent<Health>(),
            DamageUp = _damageUp,
            Heal = _heal,
            Delay = _delay
        });
    }
}
