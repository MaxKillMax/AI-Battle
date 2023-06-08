using AIBattle.Events;
using AIBattle.LiveObjects.LiveComponents.Healths;

namespace AIBattle.LiveObjects.LiveComponents.Attacks
{
    public struct AttackParameters
    {
        /// <summary>
        /// Invoke in Attack script for start animate
        /// </summary>
        public Event OnAttackStarted;
        /// <summary>
        /// Invoked for Attack script, when attack animation ended
        /// </summary>
        public Event OnAttackCompleted;

        public Event<LiveObject> OnEnemyAppeared;
        public Event<LiveObject> OnEnemyDisappered;

        public HealthAction Action;
    }
}
