using UnityEngine;

namespace AIBattle.LiveObjects.LiveComponents.Healths
{
    [CreateAssetMenu(fileName = nameof(Health), menuName = PathStart + nameof(Health), order = Order)]
    public class HealthData : LiveComponentData
    {
        [SerializeField] private float _health;
        [SerializeField] private float _maxHealth;

        public override LiveComponent Create(LiveObject liveObject) => new Health(new()
        {
            OnDestroyed = new(() => Destroy(liveObject.gameObject)),
            Amount = _health,
            MaxAmount = _maxHealth
        });
    }
}
