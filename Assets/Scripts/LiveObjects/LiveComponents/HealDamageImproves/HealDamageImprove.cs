using System;
using AIBattle.LiveObjects.LiveComponents.Attacks;
using AIBattle.LiveObjects.LiveComponents.Healths;
using AIBattle.UnityAlternatives;

namespace AIBattle.LiveObjects.LiveComponents.HealDamageImproves
{
    public class HealDamageImprove : LiveComponent
    {
        private HealthAction _heal;
        private HealthAction _damageUp;

        private readonly Health _health;
        private readonly Attack _attack;

        private readonly float _delay;
        public float Time { get; private set; }

        public HealDamageImprove(HealDamageImproveParameters parameters)
        {
            _heal = parameters.Heal;
            _damageUp = parameters.DamageUp;

            _health = parameters.Health;
            _attack = parameters.Attack;

            _delay = parameters.Delay;

            Time = _delay;

            Input.OnUpdate += ReduceTimer;
        }

        private void ReduceTimer()
        {
            Time -= UnityAlternatives.Time.Delta;

            if (Time < 0)
                Time = 0;
        }

        public void TryHeal() => DoTimeAction(() => _health.Act(_heal));

        public void TryDamage() => DoTimeAction(() => Attack.ImproveDamage(_attack, _damageUp));

        private void DoTimeAction(Action action)
        {
            if (Time > 0)
                return;

            action?.Invoke();
            Time = _delay;
        }
    }
}
