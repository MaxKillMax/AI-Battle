using AIBattle.Events;

namespace AIBattle.LiveObjects.LiveComponents.Healths
{
    public struct HealthParameters
    {
        public float Amount;
        public float MaxAmount;
        public Event OnDestroyed;
    }
}
