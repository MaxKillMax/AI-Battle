using System.Collections.Generic;
using AIBattle.Events;

namespace AIBattle.LiveObjects.LiveComponents.Aggressions
{
    /// <summary>
    /// Component search enemies in aura
    /// </summary>
    public class Aggression : LiveComponent
    {
        public bool HasEnemies => LiveObjects.Count > 0;

        private List<LiveObject> _liveObjects = new(1);

        // TODO: Temporary bad fix with removing of null objects
        public List<LiveObject> LiveObjects
        {
            get
            {
                _liveObjects.RemoveAll((l) => l == null);
                return _liveObjects;
            }
            private set
            {
                _liveObjects = value;
            }
        }

        public Event<LiveObject> OnEnemyAppeared { get; private set; } = new();
        public Event<LiveObject> OnEnemyDisappered { get; private set; } = new();

        public Aggression(AggressionParameters parameters)
        {
            parameters.OnEnemyAppeared.AddListener((l) => SetLiveObjectState(l, OnEnemyAppeared));
            parameters.OnEnemyDisappered.AddListener((l) => SetLiveObjectState(l, OnEnemyDisappered));
        }

        private void SetLiveObjectState(LiveObject liveObject, Event<LiveObject> @event)
        {
            if (!LiveObjects.Contains(liveObject))
                LiveObjects.Add(liveObject);
            else
                LiveObjects.Remove(liveObject);

            @event?.Invoke(liveObject);
        }
    }
}