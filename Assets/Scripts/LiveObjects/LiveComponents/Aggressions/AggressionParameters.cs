using AIBattle.Events;

namespace AIBattle.LiveObjects.LiveComponents.Aggressions
{
    public struct AggressionParameters
    {
        public Event<LiveObject> OnEnemyAppeared;
        public Event<LiveObject> OnEnemyDisappered;
    }
}