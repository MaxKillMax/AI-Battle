using UnityEngine;

namespace AIBattle.LiveObjects.LiveComponents.Movements
{
    [CreateAssetMenu(fileName = nameof(Movement), menuName = PathStart + nameof(Movement), order = Order)]
    public class MovementData : LiveComponentData
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _angularSpeed;
        [SerializeField] private float _acceleration;

        public override LiveComponent Create(LiveObject liveObject)
        {
            liveObject.Agent.speed = _speed;
            liveObject.Agent.angularSpeed = _angularSpeed;
            liveObject.Agent.acceleration = _acceleration;

            return new Movement(new()
            {
                Agent = liveObject.Agent
            });
        }
    }
}
