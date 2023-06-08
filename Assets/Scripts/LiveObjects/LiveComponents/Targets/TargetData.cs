using UnityEngine;

namespace AIBattle.LiveObjects.LiveComponents.Targets
{
    [CreateAssetMenu(fileName = nameof(Target), menuName = PathStart + nameof(Target), order = Order)]
    public class TargetData : LiveComponentData
    {
        [SerializeField] private TargetId _id;

        public override LiveComponent Create(LiveObject liveObject) => new Target(new()
        {
            Id = _id
        });
    }
}
