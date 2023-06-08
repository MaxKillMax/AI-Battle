using AIBattle.LiveObjects.LiveComponents.Attacks;
using AIBattle.LiveObjects.LiveComponents.Healths;

namespace AIBattle.LiveObjects.LiveComponents.HealDamageImproves
{
    public struct HealDamageImproveParameters
    {
        public float Delay;

        public HealthAction Heal;
        public HealthAction DamageUp;

        public Health Health;
        public Attack Attack;
    }
}
