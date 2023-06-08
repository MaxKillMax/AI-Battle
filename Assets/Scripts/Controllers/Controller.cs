using System.Collections.Generic;
using AIBattle.LiveObjects;
using AIBattle.LiveObjects.LiveComponents.Healths;
using AIBattle.LiveObjects.LiveComponents.Targets;

namespace AIBattle.Controllers
{
    /// <summary>
    /// So far, the logic is blank. Serves as a base class of Player, Bots and other controllers
    /// </summary>
    public abstract class Controller
    {
        protected List<LiveObject> LiveObjects { get; set; }
        public Events.Event OnAllLiveObjectDestroyed { get; private set; } = new();
        protected TargetId Id { get; private set; }

        public Controller(TargetId id, List<LiveObject> liveObjects)
        {
            Id = id;
            LiveObjects = liveObjects;

            for (int i = 0; i < LiveObjects.Count; i++)
            {
                LiveObject liveObject = LiveObjects[i];

                if (liveObject.TryGetLiveComponent(out Health health))
                    health.OnDestroyed.AddListener(() => OnLiveObjectDestroy(liveObject));
            }
        }

        private void OnLiveObjectDestroy(LiveObject liveObject)
        {
            LiveObjects.Remove(liveObject);

            if (LiveObjects.Count == 0)
                OnAllLiveObjectDestroyed?.Invoke();
        }
    }
}
