using AIBattle.Events;

namespace AIBattle.LiveObjects.LiveComponents.Healths
{
    /// <summary>
    /// Component defines liveObject can die
    /// </summary>
    public class Health : LiveComponent
    {
        public float Amount { get; private set; }
        public float MaxAmount { get; private set; }

        public Event OnAct { get; private set; } = new();
        public Event OnDestroyed { get; private set; }

        public Health(HealthParameters parameters)
        {
            Amount = parameters.Amount;
            MaxAmount = parameters.MaxAmount;
            OnDestroyed = parameters.OnDestroyed;

            CheckAmount();
        }

        public override void OnDestroy()
        {
            OnDestroyed = new();
        }

        public void Act(HealthAction action)
        {
            Amount += action.Value;

            CheckAmount();
            OnAct?.Invoke();
        }

        private void CheckAmount()
        {
            if (Amount > MaxAmount)
                Amount = MaxAmount;
            else if (Amount <= 0)
                Destroy();
        }

        public void Destroy()
        {
            Amount = 0;
            OnDestroyed.Invoke();
        }
    }
}
